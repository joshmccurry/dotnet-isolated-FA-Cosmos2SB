using Azure.Identity;
using Azure.Messaging.ServiceBus;
using jm_func_cosmosdb_to_sb.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jm_func_cosmosdb_to_sb.Services {
    public class ServiceBusClientService: IServiceBusClientService, IDisposable {
        public readonly ILogger<ServiceBusClientService> _logger;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _serviceBusSender;
        public ServiceBusClientService(ILogger<ServiceBusClientService> logger,
            String connectionString,
            String queueName) {
            _logger = logger;
            _serviceBusClient = new ServiceBusClient(connectionString);
            _serviceBusSender = _serviceBusClient.CreateSender(queueName);
        }

        public ServiceBusClientService
            (ILogger<ServiceBusClientService> logger, 
            ServiceBusCSDetails details) {
            _logger = logger;
            _serviceBusClient = new ServiceBusClient(details.ConnectionString);
            _serviceBusSender = _serviceBusClient.CreateSender(details.QueueName);
        }

        public ServiceBusClientService
            (ILogger<ServiceBusClientService> logger,
            ServiceBusIdentityDetails details) {
            _logger = logger;
            _serviceBusClient = new ServiceBusClient(details.ServiceBusNamespace, new DefaultAzureCredential());
            _serviceBusSender = _serviceBusClient.CreateSender(details.QueueName);
        }

        public ServiceBusClientService(ILogger<ServiceBusClientService> logger,
            ServiceBusClient serviceBusClient,
            ServiceBusSender serviceBusSender) {
            _logger = logger;
            _serviceBusClient = serviceBusClient;
            _serviceBusSender = serviceBusSender;
        }

        public async Task SendMessageAsync(string message) {
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(message));
        }
        public async Task SendMessagesAsync(string[] messages) {
            ServiceBusMessage[] serviceBusMessages = messages.Select(message => new ServiceBusMessage(message)).ToArray();
            await _serviceBusSender.SendMessagesAsync(serviceBusMessages);
        }
        public async Task SendMessageAsync(ServiceBusMessage message) {
            await _serviceBusSender.SendMessageAsync(message);
        }
        public async Task SendMessagesAsync(ServiceBusMessage[] messages) {
            await _serviceBusSender.SendMessagesAsync(messages);
        }

        public void Dispose() {
            _serviceBusClient.DisposeAsync();
        }
    }
}
