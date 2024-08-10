# HX-Refresh Response Header

This example demonstrates the usage of `HX-Refresh` response header to instruct the web browser to refresh the page ([doc](https://htmx.org/reference/#response_headers)).

We are using the nice utilities provided by the excellent [Htmx](https://www.nuget.org/packages/Htmx) package.

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Htmx(x =>
    {
        x.Refresh();
    });

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```