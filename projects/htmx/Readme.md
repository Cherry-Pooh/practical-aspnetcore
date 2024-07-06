# HTMX (19)

This example shows various examples on how to integrate [HTMX](https://htmx.org/) with ASP.NET Core Minimal API. We will be using [HTMX Nuget Package](https://www.nuget.org/packages/Htmx). We are using [HTMX 2](https://htmx.org/) in all samples.

## AJAX

* [all-verbs](all-verbs)

  This example shows all HTTP Verbs supported by HTMX.

* [query-string](query-string)

  This example shows how to access query string in all the HTTP Verbs supported by HTMX.

* [hx-include](hx-include)

  This example shows how to pass parameters to all supported HTTP Verbs by targeting a single `input` element using `hx-include`. 

* [hx-vals](hx-vals)

  This example shows how to pass parameters (in JSON format) to all supported HTTP Verbs using `hx-vals` . 

* [htmx-config-request](htmx-config-request)

  This examples shows how to listen to `htmx:configRequest` event to modify parameters to be sent to the server. 

* [hx-confirm](hx-confirm)

  This example shows how to use `hx-confirm` to ask for user confirmation before making a request

* [hx-prompt](hx-prompt)

  This example shows how to use `hx-prompt` to ask user for a single input before making a request

* [push-url](push-url)

  This example shows how to use `hx-push-url` to push url into browser location history.
  
## Core Attributes

* [trigger-load](trigger-load)

  This example shows how to use HTMX `hx-trigger="load"` functionality. It will call a given url when the element is loaded.

* [trigger-load-2](trigger-load-2)

  This example shows how to use HTMX `hx-trigger="load"` with `delay:1s` event modifier and `hx-swap="outerHTML"` functionalities to create self updating element. 

* [trigger-once](trigger-once)

  This example shows how to use HTMX event modifier `once` that only enable trigger to be activated one time. 

* [trigger-every](trigger-every)

  This example shows how to use HTMX `hx-trigger="every"` that activate request every specified time (polling). 

* [swap](swap)
  
  This example shows how to control where the response from the server will be swapped related to the target using `hx-swap`.

* [swap-2](swap-2)
  
  This example shows how to use `hx-swap-oob` to enable out of band swap. It is used from the server response.

* [target](target)
  
  This example shows how to specify the target element where the response from the server will be swapped using `hx-target`.

* [boost](boost)

  This example shows how to use `hx-boost` to transform HTML links and form to use AJAX request and target `body` tag.   

## Form

* [form](form)
 
  This example shows a very simple example on how to handle form submission using HTMX's `hx-post`.

* [form-2](form-2)
 
  This example shows a more complex example on how to handle form submission using HTMX's `hx-post`.

## Modals

* [modal-bootstrap](modal-bootstrap)
  
  This example shows how to show a modal dialog using HTMX and Bootstrap 5. 