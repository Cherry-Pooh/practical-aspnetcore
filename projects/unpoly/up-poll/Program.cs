var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <!DOCTYPE html>
        <html>
            <head>
                <script src="https://cdn.jsdelivr.net/npm/unpoly@3.7.3/unpoly.min.js"></script>
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/unpoly@3.7.3/unpoly.min.css">
            </head>
            <body>
                <div class="welcome-message" up-poll up-source="/unpoly" up-interval="500">
                ..wait for 0.5 seconds
                </div>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/unpoly", (HttpRequest request) =>
{
    if (request.IsUnpolyJs() is false)
        return Results.Content("");

    return Results.Content($"<div class=\"welcome-message\" up-poll up-source=\"/unpoly\" up-interval=\"500\">Hello world {DateTime.UtcNow} from UnpolyJS</div>");
});

app.Run();


public static class UnpolyJs
{
    public static class Headers
    {
        public const string UpVersion = "X-Up-Version";
    }

    public static bool IsUnpolyJs(this HttpRequest self) => !string.IsNullOrWhiteSpace(UpVersion(self));

    public static string UpVersion(HttpRequest request)
    {
        var header = request.Headers[Headers.UpVersion].ToString();

        return header;
    }
}