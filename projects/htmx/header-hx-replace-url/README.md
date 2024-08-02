# HX-Replace-Url Response Header

This example demonstrates the usage of `HX-Replace-Url` response header to replace the current url at the browser history ([doc](https://htmx.org/headers/hx-replace-url/)).

We are using the nice utilities provided by the excellent [Htmx](https://www.nuget.org/packages/Htmx) package.

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Htmx(x =>
    {
        x.ReplaceUrl("/get");
    });

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```