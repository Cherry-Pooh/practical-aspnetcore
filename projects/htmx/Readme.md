# HTMX (11)

This example shows various examples on how to integrate [HTMX](https://htmx.org/) with ASP.NET Core Minimal API. We will be using [HTMX Nuget Package](https://www.nuget.org/packages/Htmx). We are using [HTMX 2 Beta 1](https://htmx.org/posts/2024-03-15-htmx-2-0-0-beta1-is-released/) in all samples.

## AJAX

* [all-verbs](all-verbs)

  This example shows all HTTP Verbs supported by HTMX.

* [hx-include](hx-include)

  This example shows how to pass parameters to all supported HTTP Verbs by targeting a single `input` element. 

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


## Form

* [form](form)
 
  This example shows a very simple example on how to handle form submission using HTMX's `hx-post`.

* [form-2](form-2)
 
  This example shows a more complex example on how to handle form submission using HTMX's `hx-post`.
