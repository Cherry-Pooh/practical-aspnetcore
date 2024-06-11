# Processing form with HTMX hx-post

This example shows the simplest way to handle form in HTMX using `hx-post`.

```html
    <form hx-post="/simple" hx-swap="outerHTML">
        <input type="hidden" name="{ token.FormFieldName }" value="{token.RequestToken}" />
        <div class="mb-3">
            <label for="Name" class="form-label">Name</label>
            <input type="text" name="Name" class="form-control" />
        </div>
        <div class="mb-3">
            <button type="submit" class="btn btn-primary">Post</button>
        </div>
    </form>
```