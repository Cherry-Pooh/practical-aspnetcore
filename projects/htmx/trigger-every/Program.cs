using Htmx;

var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <!DOCTYPE html>
        <html>
            <head>
                <style>
                    div[hx-get]{
                        cursor:pointer;
                    }
                </style>
            </head>
            <body>
                <div hx-get="/htmx" hx-trigger="every 1s">..wait</div>
                <script src="https://unpkg.com/htmx.org@2.0.0-beta1/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx/", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content($"{DateTime.UtcNow}");
});

app.Run();


