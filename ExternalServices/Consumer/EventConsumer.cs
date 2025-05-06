using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Application.UseCases;
using ExternalServices.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Application.UseCases.Interface;
using Domain.Models;
using Application.Interface;
using ExternalServices.KafkaConfig;
using static Confluent.Kafka.ConfigPropertyNames;

namespace ExternalServices.Consumer
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ILogger<EventConsumer> _logger;
        private readonly ConsumerConfig _config;
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly IProducer _producerBus;
        private readonly IOptions<KafkaSettings> _kafkaSettings;


        public EventConsumer(IOptions<ConsumerConfig> config, IServiceScopeFactory serviceProvider, ILogger<EventConsumer> logger, IProducer producer, IOptions<KafkaSettings> kafkaSettings)
        {
            _config = config.Value;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _producerBus = producer;
            _kafkaSettings = kafkaSettings;
        }

        public async void Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                       .SetKeyDeserializer(Deserializers.Utf8)
                       .SetValueDeserializer(Deserializers.Utf8)
                       .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(3));
                if (consumeResult is null) {
                    _logger.LogInformation($"No hay mensajes a leer");
                    continue;
                }
               
                if (consumeResult.Message is null) continue;

                var options = new JsonSerializerOptions {};

                var eventObject = (topic == "CheckAvailabilityRequest_Topic" ? JsonSerializer.Deserialize<CheckAvailabilityRequestEvent>(consumeResult.Message.Value, options) 
                                               : null);

                if (eventObject is null)
                {
                    throw new ArgumentNullException("no se pudo procesar el mensaje");
                }

                _logger.LogInformation($"Mensaje recibido {consumeResult.Message.Value}");

                    List<int> recipes = new List<int>();
                    bool availableRecipes = false;

                    recipes.AddRange(eventObject.Recipes);


                    foreach (var recipeId in recipes)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var stockService = scope.ServiceProvider.GetRequiredService<IStockUC>();
                            availableRecipes =  await stockService.GetRecipeById(recipeId);

                            if (!availableRecipes)
                            {
                                availableRecipes = false;
                                break;
                            }
                    }
                    }

                //Publicar la respuesta con CorrelationId
                var eventMessagge = new CheckAvailabilityResponseEvent(eventObject.Guid, eventObject.OrderId, availableRecipes);
                await _producerBus.SendAsync(_kafkaSettings.Value.CheckAvailabilityResponseTopic, eventMessagge);

                _logger.LogInformation($"Respuesta enviada, orderId = {eventObject.OrderId} con disponibilidad = {availableRecipes}");
                               
                consumer.Commit(consumeResult);
                _logger.LogInformation($"Mensaje procesado: {consumeResult.Message.Value}");

            }

        }
    }
}
