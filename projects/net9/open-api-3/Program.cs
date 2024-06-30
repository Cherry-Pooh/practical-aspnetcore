var builder = WebApplication.CreateBuilder();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/", () =>
{
    var html = """
    <html>
    <body>
        <h1>OpenAPI JSON document</h1>
        You can check the generated OpenAPI document <a href="/openapi/v1.json">here</a>.
    </body>
    </html>
    """;

    return TypedResults.Content(html, "text/html");
}).ExcludeFromDescription(); //This is not an API endpoint

app.MapGet("/hello/{name}", (string name) => $"Hello {name}"!);

app.Run();