using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Runtime;
using Orleans.Configuration;
using Orleans.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Xml;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Linq;

await Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .ConfigureLogging(builder =>
    {
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddConsole();
    })
    .UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseInMemoryReminderService()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "http-client";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(FeedSourceGrain).Assembly).WithReferences())
            .AddRedisGrainStorage("redis-rss-reader", optionsBuilder => optionsBuilder.Configure(options =>
            {
                options.DataConnectionString = "localhost:6379";
                options.UseJson = true;
                options.DatabaseNumber = 1;
            }));
    })
    .RunConsoleAsync();

class Startup
{
    IHostEnvironment _env;

    public Startup(IHostEnvironment env) => _env = env;

    public void ConfigureServices(IServiceCollection services) => services.AddHttpClient();

    public void Configure(IApplicationBuilder app)
    {
        if (_env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                var client = context.RequestServices.GetService<IGrainFactory>()!;
                var grain = client.GetGrain<IFeedSource>(0)!;

                await grain.AddAsync(new FeedSource
                {
                    Type = FeedType.Rss,
                    Url = "http://www.scripting.com/rss.xml",
                    Website = "http://www.scripting.com",
                    Title = "Scripting News"                    
                });

                await grain.AddAsync(new FeedSource
                {
                    Type = FeedType.Atom,
                    Url = "https://www.reddit.com/r/dotnet.rss",
                    Website = "https://www.reddit.com",
                    Title = "Reddit/r/dotnet"
                });

                var sources = await grain.GetAllAsync();

                foreach(var s in sources)
                {
                    var g = client.GetGrain<IFeedFetcher>(s.Url.ToString());
                    await g.FetchAsync(s);
                }

                var resultGrain = client.GetGrain<IFeedItemResults>(0);
                var items = await resultGrain.GetAllAsync();

                await context.Response.WriteAsync(@"<html><head><link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" /></head>");
                await context.Response.WriteAsync("<body>");
                await context.Response.WriteAsync("<ul>");
                foreach(var i in items)
                {
                    await context.Response.WriteAsync("<li>");
                    if (!string.IsNullOrWhiteSpace(i.Title))
                        await context.Response.WriteAsync($"{ i.Title }<br/>");

                    await context.Response.WriteAsync(i.Description ?? "");
                    
                    if (i.Url is object)
                        await context.Response.WriteAsync($"<br/><a href=\"{i.Url}\">link</a>");

                    await context.Response.WriteAsync($"<br/><span style=\"font-size:small;\">published on: {i.PublishedOn}</span>");
                    await context.Response.WriteAsync("</li>");
                }
                await context.Response.WriteAsync("</ul>");
                await context.Response.WriteAsync("</body></html>");
            });
        });
    }
}

public class FeedItemResultGrain : Grain, IFeedItemResults
{
    private readonly IPersistentState<FeedItemStore> _storage;

    public FeedItemResultGrain([PersistentState("feed-item-results", "redis-rss-reader")] IPersistentState<FeedItemStore> storage) => _storage = storage;

    public async Task AddAsync(List<FeedItem> items)
    {
        //make sure there is no duplication
        foreach(var i in items.Where(x => !string.IsNullOrWhiteSpace(x.Id)))
        {
            if (!_storage.State.Results.Exists(x => x.Id?.Equals(i.Id, StringComparison.OrdinalIgnoreCase) ?? false))
                _storage.State.Results.Add(i);
        }
        await _storage.WriteStateAsync();
    }

    public Task<List<FeedItem>> GetAllAsync() => Task.FromResult(_storage.State.Results.OrderByDescending(x => x.PublishedOn ).ToList());

    public async Task ClearAsync()
    {
        _storage.State.Results.Clear();
        await _storage.WriteStateAsync();
    }
}

public record FeedItemStore
{
    public List<FeedItem> Results { get; set; } = new List<FeedItem>();
}

public interface IFeedItemResults : Orleans.IGrainWithIntegerKey
{
    Task AddAsync(List<FeedItem> items);

    Task<List<FeedItem>> GetAllAsync();

    Task ClearAsync();
}

public class FeedSourceGrain : Grain, IFeedSource
{
    private readonly IPersistentState<FeedSourceStore> _storage;

    public FeedSourceGrain([PersistentState("feed-source", "redis-rss-reader")] IPersistentState<FeedSourceStore> storage) => _storage = storage;

    public async Task AddAsync(FeedSource source)
    {
        if (_storage.State.Sources.Find(x => x.Url == source.Url) is null)
        {
            _storage.State.Sources.Add(source);
            await _storage.WriteStateAsync();
        }
    }

    public Task<List<FeedSource>> GetAllAsync() => Task.FromResult(_storage.State.Sources);
}

public record FeedSourceStore 
{
    public List<FeedSource> Sources { get; set; } = new List<FeedSource>();
}

public interface IFeedSource : Orleans.IGrainWithIntegerKey
{
    Task AddAsync(FeedSource source);

    Task<List<FeedSource>> GetAllAsync();
} 

public interface IFeedFetcher : Orleans.IGrainWithStringKey
{
    Task FetchAsync(FeedSource source);
}

public class FeedFetchGrain : Grain, IFeedFetcher
{
    readonly IGrainFactory _grainFactory;

    public FeedFetchGrain(IGrainFactory grainFactory) => _grainFactory = grainFactory;

    public async Task FetchAsync(FeedSource source)
    {
        var storage = _grainFactory.GetGrain<IFeedItemResults>(0);
        var results = await ReadFeedAsync(source);
        await storage.AddAsync(results);
    }

    public async Task<List<FeedItem>> ReadFeedAsync(FeedSource source)
    {
        var feed = new List<FeedItem>();

        try
        {
            using var xmlReader = XmlReader.Create(source.Url.ToString(), new XmlReaderSettings() { Async = true });
            if (source.Type == FeedType.Rss)
            {
                var feedReader = new RssFeedReader(xmlReader);

                // Read the feed
                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read Item
                        case SyndicationElementType.Item:
                            var item = await feedReader.ReadItem();
                            feed.Add(new FeedItem(source.ToChannel(), new SyndicationItem(item)));
                            break;

                        default:
                            var content = await feedReader.ReadContent();

                            break;
                    }
                }
            }
            else
            {
                var feedReader = new AtomFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read Item
                        case SyndicationElementType.Item:
                            var entry = await feedReader.ReadEntry();
                            feed.Add(new FeedItem(source.ToChannel(), new SyndicationItem(entry)));
                            break;

                        default:
                            var content = await feedReader.ReadContent();
                            break;
                    }
                }
            }

            return feed;
        }
        catch
        {
            return new List<FeedItem>();
        }
    }
}

public record FeedChannel
{
    public string? Title { get; set; }

    public string? Website { get; set; }

    public Uri? Url { get; set; }

    public bool HideTitle { get; set; }

    public bool HideDescription { get; set; }
}

public class FeedSource
{
    public string Url { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Website { get; set; }

    public FeedType Type { get; set; }

    public bool HideTitle { get; set; }

    public bool HideDescription { get; set; }

    public FeedChannel ToChannel()
    {
        return new FeedChannel
        {
            Title = Title,
            Website = Website,
            HideTitle = HideTitle,
            HideDescription = HideDescription
        };
    }
}

public record FeedItem
{
    public FeedChannel? Channel { get; set; }

    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Uri? Url { get; set;}

    public DateTimeOffset PublishedOn { get; set; }

    public FeedItem()
    {

    }

    public FeedItem(FeedChannel channel, SyndicationItem item)
    {
        Channel = channel;
        Id = item.Id;
        Title = item.Title;
        Description = item.Description;
        var link = item.Links.FirstOrDefault();
        if (link is object)
            Url = link.Uri;        

        if (item.LastUpdated == default(DateTimeOffset))
            PublishedOn = item.Published;
        else
            PublishedOn = item.LastUpdated;
    }
}

public enum FeedType
{
    Atom,
    Rss
}
