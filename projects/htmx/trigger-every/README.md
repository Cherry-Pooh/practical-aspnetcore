# HTMX polling using hx-trigger every 

This example shows `every` trigger that make request every x specified time([doc](https://htmx.org/docs/#polling)). 

```html
    <div hx-get="/htmx" hx-trigger="every 1s">..wait</div>
```

