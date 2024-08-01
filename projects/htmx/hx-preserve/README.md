# hx-preserve

This example how to use `hx-preserve` to keep an element unchanged during HTMX swap([doc](https://htmx.org/attributes/hx-preserve/))

```html
<ul>
    <li hx-get="/htmx">
        <div id="get" hx-preserve="true">GET Preserved</div>
    </li>
    <li hx-post="/htmx">
        <div id="post" hx-preserve="true">POST Preserved</div>
    </li>
    <li hx-put="/htmx">
        <div id="put" hx-preserve="true">PUT Preserved</div>
    </li>
    <li hx-patch="/htmx">
        <div id="patch" hx-preserve="true">PATCH Preserved</div>
    </li>
    <li hx-delete="/htmx">
        <div id="delete" hx-preserve="true">DELETE Preserved</div>
    </li>
</ul>
```