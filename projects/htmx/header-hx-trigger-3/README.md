# HX-Trigger Response Header

This example demonstrates the usage of `HX-Trigger` response header to emit multiple custom events at the client([doc](https://htmx.org/headers/hx-trigger/)).

We are using the nice utilities provided by the excellent [Htmx](https://www.nuget.org/packages/Htmx) package.

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Htmx(x =>
    {
        x.WithTrigger("show-me, show-you");
    });

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```