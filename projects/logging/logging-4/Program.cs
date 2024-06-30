using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Logging.AddFilter("Microsoft", LogLevel.Error);
builder.Logging.AddFilter("AppLogger", LogLevel.Error);
builder.Logging.AddConsole();

var app = builder.Build();


app.MapGet("/log-it", ([FromQuery] int number) =>
{
    var log = app.Logger;
    
    if (number % 2 == 0)
        log.LogInformationWhenInputNumberIsEven(number);
    else
        log.LogInformationWhenInputNumberIsOdd(number);

    return Results.Content("Hello world. Take a look at your terminal to see the logging messages.");
});
app.Run();