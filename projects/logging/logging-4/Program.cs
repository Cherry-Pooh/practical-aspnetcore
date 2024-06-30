using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Logging.AddFilter("Microsoft", LogLevel.Error);
builder.Logging.AddFilter("AppLogger", LogLevel.Error);
builder.Logging.AddConsole();

var app = builder.Build();

app.MapGet("/", () => 
{
    Random rng = new Random();

    List<int> numbers = [];

    foreach(var x in Enumerable.Range(0, 10))
    {
        numbers.Add(rng.Next(100));
    }

    var html = $$"""
    <!DOCTYPE html>
    <html>

    <body>
        <h1>LoggerMessage attribute</h1>
        <ul>
            {{ string.Join("", numbers.Select(x => $"<li><a href=\"/log-it?number={x}\">{x}</a></li>"))}}
        </ul>
    </body>
    </html>
    """;

    return Results.Content(html, "text/html");
});

app.MapGet("/log-it", ([FromQuery] int number) =>
{
    var log = app.Logger;
    
    if (number % 2 == 0)
        log.LogInformationWhenInputNumberIsEven(number);
    else
        log.LogInformationWhenInputNumberIsOdd(number);

    return Results.Content("Take a look at your terminal to see the logging messages.");
});

app.Run();

public static partial class EvenOddLogs
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "input number is even !: {number}")]
    public static partial void LogInformationWhenInputNumberIsEven(this ILogger logger, long number);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "input number is odd !: {number}")]
    public static partial void LogInformationWhenInputNumberIsOdd(this ILogger logger, long number);
}