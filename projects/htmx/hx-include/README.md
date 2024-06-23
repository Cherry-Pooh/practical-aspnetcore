# Using hx-include on all HTTP verbs

This example shows how to use `hx-include` targeting an input form to pass parameters to all supported HTTP verbs ([doc](https://htmx.org/attributes/hx-include/))

```html
<ul>
    <li hx-get="/htmx" hx-include="[name='Name']">GET</li>
    <li hx-post="/htmx" hx-include="[name='Name']">POST</li>
    <li hx-put="/htmx" hx-include="[name='Name']">PUT</li>
    <li hx-patch="/htmx" hx-include="[name='Name']">PATCH</li>
    <li hx-delete="/htmx" hx-include="[name='Name']">DELETE</li>
</ul>
```

On `GET` and `DELETE`, the parameters are accessible via `Request.Query`. For the rest, you can access the parameters via `Request.Form`.