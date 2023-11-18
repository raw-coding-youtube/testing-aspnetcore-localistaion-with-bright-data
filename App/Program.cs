using App;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<LumtestClient>(client => client.BaseAddress = new Uri("https://lumtest.com"));

builder.Services.AddLocalization(o => o.ResourcesPath = "Resources")
    .AddRequestLocalization(o =>
    {
        var supportedCultures = new[] {"en", "es"};
        o.SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        o.AddInitialRequestCultureProvider(new LumtestLocationProvider());
    });

var app = builder.Build();

app.UseRequestLocalization();

app.MapGet("/", ContentEndpoint.Handle);
app.MapGet("/products", ProductsEndpoint.Handle);
app.MapGet("/products/{id}", ProductEndpoint.Handle);

app.Run();