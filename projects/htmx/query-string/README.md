# Accessing query string in HTMX request

You can pass values via query string in all HTTP verbs supported by HTMX.

```html
<ul>
    <li hx-get="/htmx?name=Anna">GET</li>
    <li hx-post="/htmx?name=Anna">POST</li>
    <li hx-put="/htmx?name=Anna">PUT</li>
    <li hx-patch="/htmx?name=Anna">PATCH</li>
    <li hx-delete="/htmx?name=Anna">DELETE</li>
</ul>
```