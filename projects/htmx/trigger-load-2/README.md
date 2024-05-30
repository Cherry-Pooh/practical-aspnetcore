# Load polling hx-trigger load

This example shows `load` event trigger ([doc](https://v2-0v2-0.htmx.org/docs/#special-events)) with `delay` and [`hx-swap`](https://v2-0v2-0.htmx.org/attributes/hx-swap/). 

```html
<div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML"></div>
```

`hx-swap="outerHTML` tells HTMX to replace the entire target element with the response. You will see at the API that we return the same exect element with additional content. HTMX will them process this new content and make another call in (so on and so forth).

```csharp
    return Results.Content($"""<div hx-get="/htmx" hx-trigger="load delay:1s" hx-swap="outerHTML">{DateTime.UtcNow}</div>""");
```


