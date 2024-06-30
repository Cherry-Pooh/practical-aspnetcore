# OpenAPI Document Generator

This example shows how to use the built in support for generating OpenAPI document. In the previous version of ASP.NET Core you have to rely third party packages such as [NSwag](https://github.com/RicoSuter/NSwag) to do so.

You can see that we use `.ExcludeFromDescription();` to exclude `MapGet("\")` from being described in the generated OpenAPI document.