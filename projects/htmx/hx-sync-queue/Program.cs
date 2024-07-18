using Htmx;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
var app = builder.Build();

app.UseAntiforgery();

app.MapGet("/", (HttpContext context, [FromServices] IAntiforgery anti) =>
{
    var token = anti.GetAndStoreTokens(context);

    var html = $$"""
        <!DOCTYPE html>
        <html>
            <head>
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
                <style>
                    li{
                        cursor:pointer;
                    }
                </style>
                <meta name="htmx-config" content='{ "antiForgery": {"headerName" : "{{ token.HeaderName}}", "requestToken" : "{{token.RequestToken }}" } }'>
            </head>
            <body>
            <h1>All verbs supported in HTMX</h1>
            <p>Click on the below links to see the response</p>
            <div class="row">
                <div class="col-md-4">
                    <h2>hx-sync="this:queue first"</h2>
                    <ul hx-sync="this:queue first" hx-trigger="increment">
                        <li hx-get="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>GET</li>
                        <li hx-post="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>POST</li>
                        <li hx-put="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>PUT</li>
                        <li hx-patch="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>PATCH</li>
                        <li hx-delete="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>DELETE</li>
                    </ul>
                </div>
                <div class="col-md-4">
                    <h2>hx-sync="this:queue last"</h2>
                    <ul hx-sync="this:queue last" hx-trigger="increment">
                        <li hx-get="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>GET</li>
                        <li hx-post="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>POST</li>
                        <li hx-put="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>PUT</li>
                        <li hx-patch="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>PATCH</li>
                        <li hx-delete="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}'>DELETE</li>
                    </ul>
                </div>
                <div class="col-md-4">
                    <h2>hx-sync="this:queue all"</h2>
                    <ul hx-sync="this:queue all" hx-trigger="increment">
                        <li><a hx-get="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-get">GET</a> <span id="all-get"></span></li>
                        <li><a hx-post="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-post">POST</a> <span id="all-post"></span></li>
                        <li><a hx-put="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-put">PUT</a> <span id="all-put"></span></li>
                        <li><a hx-patch="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-patch">PATCH</a> <span id="all-patch"></span></li>
                        <li><a hx-delete="/htmx" hx-on:click="incrementCount(this, event)" hx-vals='{"count" : 1}' hx-target="#all-delete">DELETE</a> <span id="all-delete"></span></li>
                    </ul>
                </div>
            </div>

            <script src="https://unpkg.com/htmx.org@2.0.0" integrity="sha384-wS5l5IKJBvK6sPTKa2WZ1js3d947pvWXbPJ1OmWfEuxLgeHcEbjUUA5i9V5ZkpCw" crossorigin="anonymous"></script>
            <script>
                function incrementCount(target, event) {
                    event.preventDefault();
                    let vals = JSON.parse(target.getAttribute('hx-vals'));
                    vals.count = vals.count + 1;
                    console.log('vals.count', vals.count);
                    target.setAttribute('hx-vals', JSON.stringify(vals));
                    htmx.trigger(target, 'increment');
                }

                document.addEventListener("htmx:configRequest", (evt) => {
                    let httpVerb = evt.detail.verb.toUpperCase();
                    if (httpVerb === 'GET') return;
                    
                    let antiForgery = htmx.config.antiForgery;
                    if (antiForgery) {
                        // already specified on form, short circuit
                        if (evt.detail.parameters[antiForgery.formFieldName])
                            return;
                        
                        if (antiForgery.headerName) {
                            evt.detail.headers[antiForgery.headerName]
                                = antiForgery.requestToken;
                        } else {
                            evt.detail.parameters[antiForgery.formFieldName]
                                = antiForgery.requestToken;
                        }
                    }
                });
            </script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

var htmx = app.MapGroup("/htmx").AddEndpointFilter(async (context, next) =>
{
    if (context.HttpContext.Request.IsHtmx() is false)
        return Results.Content("");

    if (context.HttpContext.Request.Method == "GET")
        return await next(context);

    await context.HttpContext.RequestServices.GetRequiredService<IAntiforgery>()!.ValidateRequestAsync(context.HttpContext);
    return await next(context);
});

htmx.MapGet("/", async (HttpRequest request) =>
{
    await Task.Delay(1000);
    return Results.Content($"GET => {DateTime.UtcNow} =>" + request.Query["count"]);
});

htmx.MapPost("/", async (HttpRequest request) =>
{
    await Task.Delay(1000);
    return Results.Content($"POST => {DateTime.UtcNow} =>" + request.Form["count"]);
});

htmx.MapDelete("/", async (HttpRequest request) =>
{
    await Task.Delay(1000);
    return Results.Content($"DELETE => {DateTime.UtcNow} => " + request.Query["count"]);
});

htmx.MapPut("/", async (HttpRequest request) =>
{
    await Task.Delay(1000);
    return Results.Content($"PUT => {DateTime.UtcNow} => "  + request.Form["count"]);
});

htmx.MapPatch("/", async (HttpRequest request) =>
{
    await Task.Delay(1000);
    return Results.Content($"PATCH => {DateTime.UtcNow} => "  + request.Form["count"]);
});

app.Run();