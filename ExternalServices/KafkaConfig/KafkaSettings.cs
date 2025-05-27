using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalServices.KafkaConfig
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string CheckAvailabilityRequestTopic { get; set; } = string.Empty;
        public string CheckAvailabilityResponseTopic { get; set; } = string.Empty;
        public string UpdateStockRequestTopic { get; set; } = string.Empty;
        public string UpdatedStockResponseTopic { get; set; } = string.Empty;
    }
}
