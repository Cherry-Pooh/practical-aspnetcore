# Load multiple fragments of content via up-target

We are using the `up-target` attribute as documented [here](https://unpoly.com/up.link). In this example we use tag, id, and class selectors all at the same time.

```html
    <div class="col-md-3">
        <nav>
            <a href="/unpoly/1" up-target="article, #show, .show">update all</a>
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

