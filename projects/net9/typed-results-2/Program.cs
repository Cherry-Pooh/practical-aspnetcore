var app = WebApplication.Create();

app.MapGet("/", () => TypedResults.InternalServerError("Something is wrong with the server."));

app.Run();