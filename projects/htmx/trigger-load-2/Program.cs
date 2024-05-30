using Htmx;

var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <html>
            <body>
                <div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML"></div>

                <script src="https://unpkg.com/htmx.org@2.0.0-beta1/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content($"""<div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML">{DateTime.UtcNow}</div>""");
});

app.Run();


