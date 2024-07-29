using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Workbook.API.Configurations;
using Workbook.API.Services;

var host = new HostBuilder();
#if DEBUG
host.ConfigureAppConfiguration(configDelegate =>
{
    configDelegate.AddJsonFile("local.settings.json");
});
#endif
host.ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<IOpenApiConfigurationOptions, OpenApiConfigurations>();
        services.AddSingleton<WorkbookServiceConfiguration>();
        services.AddHttpClient<WorkbookClient>();
        services.AddSingleton<WorkbookRepository>();
    });

var app = host.Build();

app.Run();
