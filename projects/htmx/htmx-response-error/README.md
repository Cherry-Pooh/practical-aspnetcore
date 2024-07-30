# Listen to htmx:responseError event to obtain AJAX error message

This example shows how to listen to `htmx:responseError` to obtain AJAX error message from the server([doc](https://htmx.org/events/#htmx:responseError))

```js
    document.addEventListener("htmx:responseError", (evt) => {
        console.log("event", evt);
        alert(evt.detail.xhr.status + ":" + evt.detail.xhr.statusText);
    });
```