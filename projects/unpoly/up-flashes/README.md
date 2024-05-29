# Load flash message using up-flashes

We are using the `up-flashes` attribute as documented [here](https://unpoly.com/flashes). 

```html
    <div class="col-md-3">
        <nav>
            <a href="/unpoly/1" up-target="article:maybe" up-cache="false">1st</a>
            <a href="/unpoly/2" up-target="article:maybe" up-cache="false">2nd</a>
        </nav>
    </div>
    <div class="col-md-3">
        <article>Default Content 1</article>
    </div>
    <div class="col-md-3">
        <div up-flashes></div>
    </div>
```

> [!NOTE]
>
> We use `up-cache` to prevent unpoly from caching the content.
> We also set the selector with `:maybe` to tell unpoly not to throw error because we are not returning any matching element for article. 