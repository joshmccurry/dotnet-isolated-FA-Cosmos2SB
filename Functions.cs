using System;
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using jm_func_cosmosdb_to_sb.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace jm_func_cosmosdb_to_sb
{
    public class Functions
    {
        private readonly ILogger _logger;
        private readonly IServiceBusClientService _sbService;

        public Functions(ILoggerFactory loggerFactory, IServiceBusClientService sbService)
        {
            _logger = loggerFactory.CreateLogger<Functions>();
            _sbService = sbService;
        }

        [Function("Function1")]
        public void Run([CosmosDBTrigger(
            databaseName: "databaseName",
            containerName: "containerName",
            Connection = "CosmosConnectionString",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<MyDocument> input)
        {
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("First document Id: " + input[0].id);
            }
            ServiceBusMessage msg = new ServiceBusMessage(input[0].Text);
            _sbService.SendMessageAsync(msg);
        }
    }

    public class MyDocument
    {
        public string id { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public bool Boolean { get; set; }
    }
}
