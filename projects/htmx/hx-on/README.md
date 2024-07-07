# Using hx-on to handle events

This example shows how to use `hx-on` to handle HTML Events using inline JavaScript code ([doc](https://htmx.org/attributes/hx-on/))

```html
    <ul>
        <li hx-on:click="alert('click');">hx-on:click</li>
        <li hx-on:mouseover="alert('hover');">hx-on:mouseover</li>
        <li hx-on:dblclick="alert('double click');">hx-on:dblclick</li>
    </ul>
```

`hx-on` handles [HTML Events](https://www.w3schools.com/tags/ref_eventattributes.asp) and [HTMX Events](https://htmx.org/reference/#events).