using Htmx;

var app = WebApplication.Create();
app.Map("/", () =>
{
    var html = """
        <html>
            <body>
                <div hx-get="/htmx" hx-trigger="load"></div>

                <script src="https://unpkg.com/htmx.org@2.0.0-beta1/dist/htmx.min.js"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.Map("/htmx", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content("Hello world from HTMX");
});

app.Run();


