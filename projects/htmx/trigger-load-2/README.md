# Load polling using hx-trigger load with delay

This example shows `load` event trigger ([doc](https://htmx.org/docs/#special-events)) with `delay` event modifier and [`hx-swap`](https://htmx.org/attributes/hx-swap/). 

```html
<div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML"></div>
```

`hx-swap="outerHTML` tells HTMX to replace the entire target element with the response. You will see at the API that we return the same exect element with additional content. HTMX will them process this new content and make another call in (so on and so forth).

```csharp
    return Results.Content($"""<div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML">{DateTime.UtcNow}</div>""");
```


