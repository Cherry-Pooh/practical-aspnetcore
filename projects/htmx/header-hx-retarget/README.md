# HX-Trigger Response Header

This example demonstrates the usage of `HX-Retarget` response header to retarget an element (overriding the request) at the client using CSS selector([doc](https://htmx.org/reference/#events)).

We are using the nice utilities provided by the excellent [Htmx](https://www.nuget.org/packages/Htmx) package.

```csharp
htmx.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
    response.Htmx(x =>
    {
        x.Retarget("#get");
    });

    return Results.Content($"GET => {DateTime.UtcNow}");
});
```