# hx-replace-url

This example shows how to use `hx-replace-url` with custom url to replace the current url of the browser location ([doc](https://htmx.org/attributes/hx-replace-url/))

```html
    <ul>
        <li hx-get="/htmx/get" hx-replace-url="/person/anna">GET</li>
        <li hx-post="/htmx/post" hx-replace-url="/person/john">POST</li>
        <li hx-put="/htmx/put" hx-replace-url="/person/ahmed">PUT</li>
        <li hx-patch="/htmx/patch" hx-replace-url="/person/gaby">PATCH</li>
        <li hx-delete="/htmx/delete" hx-replace-url="/person/daniela">DELETE</li>
    </ul>
```