using jm_func_cosmosdb_to_sb.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using jm_func_cosmosdb_to_sb.Models;
using Microsoft.Azure.Functions.Worker;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
builder.Services
     .AddApplicationInsightsTelemetryWorkerService()
     .ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton(new ServiceBusCSDetails() {
    ConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString"),
    QueueName = Environment.GetEnvironmentVariable("ServiceBusQueueName")
}
    );
builder.Services.AddSingleton<IServiceBusClientService, ServiceBusClientService>();

builder.Build().Run();
