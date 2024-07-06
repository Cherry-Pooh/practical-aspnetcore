# hx-push-url attribute

This example shows how to use `hx-push-url` to push a URL into the browser location history ([doc](https://htmx.org/attributes/hx-push-url/))

```html
    <ul hx-push-url="true">
        <li hx-get="/htmx/get">GET</li>
        <li hx-post="/htmx/post">POST</li>
        <li hx-put="/htmx/put">PUT</li>
        <li hx-patch="/htmx/patch">PATCH</li>
        <li hx-delete="/htmx/delete">DELETE</li>
    </ul>
```