var app = WebApplication.Create();

app.MapGet("/", () => Results.Extensions.RazorSlice<HelloWorld.Slices.Index, string>("Hello world"));

app.Run();


