# hx-replace-url

This example shows how to use `hx-replace-url` to replace the current url of the browser location ([doc](https://htmx.org/attributes/hx-replace-url/))

```html
    <ul>
        <li hx-get="/htmx/get" hx-replace-url="true">GET</li>
        <li hx-post="/htmx/post" hx-replace-url="true">POST</li>
        <li hx-put="/htmx/put" hx-replace-url="true">PUT</li>
        <li hx-patch="/htmx/patch" hx-replace-url="true">PATCH</li>
        <li hx-delete="/htmx/delete" hx-replace-url="true">DELETE</li>
    </ul>
```