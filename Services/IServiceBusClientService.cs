using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jm_func_cosmosdb_to_sb.Services {
    public interface IServiceBusClientService {
        public Task SendMessageAsync(string message);
        public Task SendMessagesAsync(string[] message);
        public Task SendMessageAsync(ServiceBusMessage message);
        public Task SendMessagesAsync(ServiceBusMessage[] message);
    }
}
