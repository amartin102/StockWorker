﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExternalServices.Common;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
//using static Confluent.Kafka.ConfigPropertyNames;


namespace ExternalServices.KafkaConfig
{
    public class Producer : IProducer
    {
        private readonly KafkaSettings _kafkaSettings;
        private readonly ILogger<Producer> _logger;

        public Producer(IOptions<KafkaSettings> kafkaSettings, ILogger<Producer> logger)
        {
            _kafkaSettings = kafkaSettings.Value;
            _logger = logger;
        }



        public async Task SendAsync<T>(string topic, T customerEvent)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers
                //SecurityProtocol = Enum.TryParse(_kafkaSettings.SecurityProtocol, out SecurityProtocol securityProtocol) ? securityProtocol : SecurityProtocol.Plaintext,
                //SaslMechanism = Enum.TryParse(_kafkaSettings.SaslMechanism, out SaslMechanism saslMechanism) ? saslMechanism : SaslMechanism.Plain,
                //SaslUsername = _kafkaSettings.SaslUsername,
                //SaslPassword = _kafkaSettings.SaslPassword,
                //SslEndpointIdentificationAlgorithm = _kafkaSettings.sslendpointidentificationalgorithm.ToString() == "None" ? SslEndpointIdentificationAlgorithm.None : SslEndpointIdentificationAlgorithm.Https,

            };

            using var producer = new ProducerBuilder<string, string>(config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

            var eventMessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(customerEvent)
            };

            var deliveryStatus = await producer.ProduceAsync(topic, eventMessage);

            if (deliveryStatus.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception(@$"
            No se pudo enviar el mensaje {customerEvent.GetType().Name} 
            hacia el topic - {topic}, 
            por la siguiente razon: {deliveryStatus.Message}");
            }
            _logger.LogInformation("Event sent!");

        }

    }

}
