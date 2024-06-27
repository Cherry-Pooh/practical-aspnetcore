# Ask for a prompt using hx-prompt

This example shows how to use `hx-prompt` to ask a user for an input before making a request ([doc](https://htmx.org/attributes/hx-prompt/))

```html
    <ul hx-prompt="what is your name?">
        <li hx-get="/htmx">GET</li>
        <li hx-post="/htmx">POST</li>
        <li hx-put="/htmx">PUT</li>
        <li hx-patch="/htmx">PATCH</li>
        <li hx-delete="/htmx">DELETE</li>
    </ul>
```

You can see here that `hx-prompt`, like many HTMX attributes, support inheritance. The HTML above has the same functional equivalent of the HTML below

```html
<ul>
    <li hx-get="/htmx" hx-prompt="what is your name?">GET</li>
    <li hx-post="/htmx" hx-prompt="what is your name?">POST</li>
    <li hx-put="/htmx" hx-prompt="what is your name?">PUT</li>
    <li hx-patch="/htmx" hx-prompt="what is your name?">PATCH</li>
    <li hx-delete="/htmx" hx-prompt="what is your name?">DELETE</li>
</ul>
```
