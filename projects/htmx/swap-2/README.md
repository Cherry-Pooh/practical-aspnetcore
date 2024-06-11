# HTMX hx-swap-oob to perform out of band swap 

This example shows how to enable out of band swap using `hx-swap-oob` ([doc](https://v2-0v2-0.htmx.org/docs/#oob_swaps)). 

The attribute is used in the response by the server. 

```csharp
    return key switch 
    {
        "innerHtml" => Results.Content($"""
        Hello {DateTime.UtcNow}. <p>Because this is hxSwap="innerHTML", you can keep clicking and the swap keeps working. Check the date. </p>
        <div id="oob-target" hx-swap-oob="true">New out of band message {DateTime.UtcNow}</div>
        """),
        _ => Results.Content("")
    };
```