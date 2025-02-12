using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jm_func_cosmosdb_to_sb.Models {
    public class ServiceBusCSDetails {
        public String ConnectionString {
            get; set;
        }
        public String QueueName {
            get; set;
        }
    }
}
