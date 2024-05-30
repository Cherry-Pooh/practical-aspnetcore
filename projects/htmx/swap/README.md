# HTMX hx-swap

This example shows how to control where the response from the server will be swapped related to the target using `hx-swap` ([doc](https://v2-0v2-0.htmx.org/attributes/hx-swap/)). 

```html
    <div class="row">
        <div class="col-md-3 m-3">
            <strong>innerHTML</strong><br/>
            <div hx-get="/htmx/innerHtml" hx-trigger="click" hx-swap="innerHTML">Click Me</div>
        </div>
        <div class="col-md-3 m-3">
            <strong>outerHTML</strong><br/>
            <div hx-get="/htmx/outerHTML" hx-trigger="click" hx-swap="outerHTML">Click Me</div>
        </div>
            <div class="col-md-3 m-3">
            <strong>textContent</strong><br/>
            <div hx-get="/htmx/textContent" hx-trigger="click" hx-swap="textContent">Click Me</div>
        </div>
        <div class="col-md-3 m-3">
            <strong>beforebegin</strong><br/>
            <div hx-get="/htmx/beforebegin" hx-trigger="click" hx-swap="beforebegin">Click Me</div>
        </div>
        <div class="col-md-3 m-3">
            <strong>afterbegin</strong><br/>
            <div hx-get="/htmx/afterbegin" hx-trigger="click" hx-swap="afterbegin">Click Me</div>
        </div>
        <div class="col-md-3 m-3">
            <strong>beforeend</strong><br/>
            <div hx-get="/htmx/beforeend" hx-trigger="click" hx-swap="beforeend">Click Me</div>
        </div>
        <div class="col-md-3 m-3">
            <strong>afterend</strong><br/>
            <div hx-get="/htmx/afterend" hx-trigger="click" hx-swap="afterend">Click Me</div>
        </div>
        <div class="col-md-3 m-3">
            <strong>delete</strong><br/>
            <div hx-get="/htmx/delete" hx-trigger="click" hx-swap="delete">Click Me</div>
        </div>
        <div class="col-md-3 m-3">
            <strong>none</strong><br/>
            <div hx-get="/htmx/none" hx-trigger="click" hx-swap="none">Click Me</div>
        </div>
    </div>
```

