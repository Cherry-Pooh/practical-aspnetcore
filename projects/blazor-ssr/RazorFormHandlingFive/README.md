# Validation on Blazor SSR Form using DataAnnotationsValidator and EditForm

This example shows how to perform data validation using `DataAnnotationsValidator` and `EditForm`.


## Important

Blazor SSR does not allow class with multiple constructors to be used as a model. This also applies to nested objects (https://github.com/dotnet/aspnetcore/issues/55711).