# Polling in specific interval using up-poll

We are using the `up-poll` attribute as documented [here](https://unpoly.com/up-poll).

```html
    <div class="welcome-message" up-poll up-source="/unpoly" up-interval="500">
    ..wait for 0.5 seconds
    </div>
```

[!NOTE]
It is importat that the API returns a matching response otherwise you will not get the desired result (polling).