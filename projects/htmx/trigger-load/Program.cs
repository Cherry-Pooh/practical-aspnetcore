using Htmx;

var app = WebApplication.Create();
app.Map("/", () =>
{
    var html = """
        <html>
            <body>
                <div hx-get="/htmx" hx-trigger="load"></div>

                <script src="https://unpkg.com/htmx.org@1.9.12" integrity="sha384-ujb1lZYygJmzgSwoxRggbCHcjc0rB2XoQrxeTUQyRjrOnlCoYta87iKBWq3EsdM2" crossorigin="anonymous"></script>
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


