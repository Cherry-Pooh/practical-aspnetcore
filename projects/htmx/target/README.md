# HTMX hx-target

This example shows how to specify the target element where the response from the server will be swapped using `hx-target` ([doc](https://v2-0v2-0.htmx.org/docs/#targets)). 

```html
    <ul>
        <li>
            <strong>this keyword</strong><br/>
            <div hx-get="/htmx/this-keyword" hx-trigger="click" hx-target="this">Click Me</div>
        </li>
        <li>
            <strong>Tag Selector</strong><br/>
            <div hx-get="/htmx/tag-selector" hx-trigger="click" hx-target="article">Click Me</div>
        </li>
        <li>
            <strong>Id Selector</strong><br/>
            <div hx-get="/htmx/id-selector" hx-trigger="click" hx-target="#message">Click Me</div>
        </li>
        <li>
            <strong>Class Selector</strong><br/>
            <div hx-get="/htmx/class-selector" hx-trigger="click" hx-target=".info">Click Me</div>
        </li>
    </ul>
```

