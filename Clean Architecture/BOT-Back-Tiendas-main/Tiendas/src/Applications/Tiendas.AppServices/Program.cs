using credinet.comun.api;
using credinet.comun.api.Swagger.Extensions;
using credinet.exception.middleware;
using Helpers.ObjectsUtils;
using Helpers.ObjectsUtils.Setting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using SC.Configuration.Provider.Mongo;
using Serilog;
using System.IO;
using System.Linq;
using Tiendas.AppServices.Extensions;
using Tiendas.AppServices.Extensions.Health;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddMongoProvider(
    nameof(MongoConfigurationProvider), secretos.MongoConnection, country);

#region Service Configuration

string policyName = "cors";
builder.Services
    .RegisterCors(policyName)
    .RegisterAutoMapper()
    .RegisterMongo(secretos.MongoConnection, $"{appSettings.Database}_{country}")
    .RegisterAsyncGateways(secretos.ServicesBusConnection)
    .RegisterServices()
    .AddVersionedApiExplorer()
    .HabilitarVesionamiento()
    .ConfigurarSwaggerConVersiones(builder.Environment, PlatformServices.Default.Application.ApplicationBasePath,
        new string[] { "Tiendas.AppServices.xml" });

builder.Services
    .AddHealthChecks()
    .AddMongoDb(secretos.MongoConnection, name: "MongoDB");

#endregion Service Configuration

WebApplication app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger((c) =>
    {
        c.PreSerializeFilters.Add((swaggerDoc, httpRequest) => { swaggerDoc.Info.Description = httpRequest.Host.Value; });
    });
    app.UseSwaggerUI(options =>
    {
        foreach (string description in provider.ApiVersionDescriptions.Select(description => description.GroupName))
        {
            options.SwaggerEndpoint($"../swagger/{description}/swagger.json", description.ToUpperInvariant());
        }
        options.InjectStylesheet($"../swagger.{app.Environment.EnvironmentName}.css");
    });
}

// Enable middleware to serve generated Swagger as a JSON endpoint.

app.UseRouting();
app.UseCors(policyName);
app.UseStaticFiles();
app.ServiceHealthChecks(appSettings.HealthChecksEndPoint, appSettings.DomainName);
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseAmbienteHeaderMiddleware();
app.UseOrigenHeaderMiddleware();
app.MapControllers();
app.Run();