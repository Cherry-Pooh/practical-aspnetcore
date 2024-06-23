using Htmx;

var app = WebApplication.Create();
app.MapGet("/", () =>
{
    var html = """
        <!DOCTYPE html>
        <html>
            <head>
                <style>
                    li{
                        cursor:pointer;
                    }
                </style>
            </head>
            <body>
            <h1>Passing parameters to all HTTP verbs via hx-vals</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx" hx-vals='{"Name": "Anna"}'>GET</li>
                <li hx-post="/htmx" hx-vals='{"Name": "Anna"}'>POST</li>
                <li hx-put="/htmx" hx-vals='{"Name": "Anna"}'>PUT</li>
                <li hx-patch="/htmx" hx-vals='{"Name": "Anna"}'>PATCH</li>
                <li hx-delete="/htmx" hx-vals='{"Name": "Anna"}'>DELETE</li>
            </ul>
                <script src="https://unpkg.com/htmx.org@2.0.0" integrity="sha384-wS5l5IKJBvK6sPTKa2WZ1js3d947pvWXbPJ1OmWfEuxLgeHcEbjUUA5i9V5ZkpCw" crossorigin="anonymous"></script>
            </body>
        </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/htmx/", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content($"GET => {DateTime.UtcNow} + {request.Query["Name"]}");
});

app.MapPost("/htmx/", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content($"POST => {DateTime.UtcNow} + {request.Form["Name"]}");
});

app.MapDelete("/htmx/", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content($"DELETE => {DateTime.UtcNow} + {request.Query["Name"]}");
});

app.MapPut("/htmx/", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content($"PUT => {DateTime.UtcNow} + {request.Form["Name"]}");
});

app.MapPatch("/htmx/", (HttpRequest request) =>
{
    if (request.IsHtmx() is false)
        return Results.Content("");

    return Results.Content($"PATCH => {DateTime.UtcNow} + {request.Form["Name"]}");
});

app.Run();


