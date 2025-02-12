using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using jm_func_cosmosdb_to_sb.Models;
using jm_func_cosmosdb_to_sb.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
     .AddApplicationInsightsTelemetryWorkerService()
     .ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton(new ServiceBusCSDetails() {
    ConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString"),
    QueueName = Environment.GetEnvironmentVariable("ServiceBusQueueName")
});

//For Managed Identity Connections
//builder.Services.AddSingleton(new ServiceBusIdentityDetails() {
//    ServiceBusNamespace = Environment.GetEnvironmentVariable("ServiceBusNameSpace"),
//    QueueName = Environment.GetEnvironmentVariable("ServiceBusQueueName")
//});

builder.Services.AddSingleton<IServiceBusClientService, ServiceBusClientService>();

builder.Build().Run();
