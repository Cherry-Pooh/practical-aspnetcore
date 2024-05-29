# Load fragment of content via up-target

We are using the `up-target` attribute as documented [here](https://unpoly.com/up.link). In this example we use tag, id, and class selectors.

```html
    <div class="col-md-3">
        <nav>
            <a href="/unpoly/1" up-target="article">1st</a>
            <a href="/unpoly/2" up-target="#show">2nd</a>
            <a href="/unpoly/3" up-target="#show">3rd</a>
            <a href="/unpoly/4" up-target=".show">4th</a>
            <a href="/unpoly/5" up-target=".show">5th</a>
        </nav>
    </div>
    <div class="col-md-3">
        <article>Default Content 1</article>
    </div>
    <div class="col-md-3">
        <div id="show">Default Content 2</div>
    </div>
    <div class="col-md-3">
        <div class="show">Default Content 3</div>
    </div>
```

>[!NOTE]
>
>It is importat that the API returns a matching target otherwise you will not get the desired result.

```csharp
    return Results.Content($"<span id=\"show\"><strong>{text}</strong> {DateTime.UtcNow} from UnpolyJS</span>");
```

If in above example you change the `id` value to the one not specified in `up-target`, you will not get the right result. 