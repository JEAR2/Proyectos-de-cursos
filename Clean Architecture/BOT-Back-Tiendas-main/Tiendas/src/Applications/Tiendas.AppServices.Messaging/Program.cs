using EntryPoints.ServicesBus.Tienda;
using Helpers.ObjectsUtils;
using Helpers.ObjectsUtils.Setting;
using Serilog;
using Tiendas.AppServices.Messaging.Extensions;
using Tiendas.AppServices.Messaging.Extensions.Health;

var builder = WebApplication.CreateBuilder(args);

#region Host Configuration

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonProvider();

//HACK: Para usar fuera de Siste.
//builder.Configuration.AddKeyVaultProvider();

builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console()
       .ReadFrom.Configuration(ctx.Configuration));

#endregion Host Configuration

builder.Services.Configure<ConfiguradorAppSettings>(builder.Configuration.GetRequiredSection(nameof(ConfiguradorAppSettings)));
ConfiguradorAppSettings appSettings = builder.Configuration.GetSection(nameof(ConfiguradorAppSettings)).Get<ConfiguradorAppSettings>();
//HACK: Para usar fuera de Siste.
//Secrets secrets = builder.Configuration.ResolveSecrets<Secrets>();
Secrets secretos = builder.Configuration.GetSection(nameof(Secrets)).Get<Secrets>();
string country = EnvironmentHelper.GetCountryOrDefault(appSettings.DefaultCountry);

#region Service Configuration

builder.Services
    .RegisterAutoMapper()
    .RegisterMongo(secretos.MongoConnection, $"{appSettings.Database}_{country}")
    .RegisterAsyncGateways(secretos.ServicesBusConnection)
    .RegisterServices()
    .RegisterSubscriptions();

builder.Services
    .AddHealthChecks()
    .AddMongoDb(secretos.MongoConnection, name: "MongoDB");

#endregion Service Configuration

var app = builder.Build();

var appTiendaCommandSubscription =
    app.Services.GetRequiredService<ITiendaCommandSubscription>();

var appTiendaEventSubscription =
    app.Services.GetRequiredService<ITiendaEventSubscription>();

appTiendaCommandSubscription.SubscribeAsync().Wait();
appTiendaEventSubscription.SubscribeAsync().Wait();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.ServiceHealthChecks(appSettings.HealthChecksEndPoint, appSettings.DomainName);

app.Run();