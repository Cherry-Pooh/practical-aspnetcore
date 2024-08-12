# Listen to htmx:afterOnLoad event  

This example shows how to listen to `htmx:afterOnLoad` ([doc](https://htmx.org/events/#htmx:afterOnLoad)).

> This event is triggered after an AJAX onload has finished. Note that this does not mean that the content has been swapped or settled yet, only that the request has finished.

```js
    document.addEventListener("htmx:afterOnLoad", (evt) => {
        let li = evt.detail.elt;
        alert(li.id);
    });
```