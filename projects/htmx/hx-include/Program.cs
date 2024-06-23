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
            <h1>Passing parameters to all HTTP verbs via hx-include targeting an input form</h1>
            <p>Click on the below links to see the response</p>
            <ul>
                <li hx-get="/htmx" hx-include="[name='Name']">GET</li>
                <li hx-post="/htmx" hx-include="[name='Name']">POST</li>
                <li hx-put="/htmx" hx-include="[name='Name']">PUT</li>
                <li hx-patch="/htmx" hx-include="[name='Name']">PATCH</li>
                <li hx-delete="/htmx" hx-include="[name='Name']">DELETE</li>
            </ul>
                <h2>Please fill this input</h2>
                <label for="Name">Name:</label></br>
                <input type="text" name="Name" id="Name"/> <br/>

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


