var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <!DOCTYPE html>
        <html>
            <head>
                <script src="https://cdn.jsdelivr.net/npm/unpoly@3.7.3/unpoly.min.js"></script>
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
                <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/unpoly@3.7.3/unpoly.min.css">
            </head>
            <body>
                <div class="container mt-5">
                    <div class="row">
                        <div class="col-md-3">
                            <nav>
                                <a href="/unpoly/1" up-target="article:maybe" up-cache="false">1st</a>
                                <a href="/unpoly/2" up-target="article:maybe" up-cache="false">2nd</a>
                            </nav>
                        </div>
                        <div class="col-md-3">
                            <article>Default Content 1</article>
                        </div>
                        <div class="col-md-3">
                            <div up-flashes></div>
                        </div>
                    </div>
                </div>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});


int clicked = 0;

app.MapGet("/unpoly/{idx}", (HttpRequest request, int idx) =>
{
    if (request.IsUnpolyJs() is false)
        return Results.Content("");

    return Results.Content($"""
        <div class="alert alert-success" up-flashes>You have clicked {clicked++} times at {idx}</div>
        """);
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