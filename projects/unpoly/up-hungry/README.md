# Load fragment of content on untargeted element via up-hungry

We are using the `up-hungry` attribute as documented [here](https://unpoly.com/up-hungry). In this example we use a tag selector.

```html
    <div class="row">
        <div class="col-md-3">
            <nav>
                <a href="/unpoly/1" up-target="article">1st</a>
            </nav>
        </div>
        <div class="col-md-3">
            <article>Default Content 1</article>
        </div>
        <div class="col-md-3">
            <div id="show" up-hungry>Default Content 2</div>
        </div>
</div>
```