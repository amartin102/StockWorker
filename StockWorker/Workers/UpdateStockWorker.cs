using System.Diagnostics.CodeAnalysis;
using Confluent.Kafka;
using ExternalServices.Common;
using ExternalServices.Consumer;
using Microsoft.Extensions.Options;

namespace Worker.Workers
{
    [ExcludeFromCodeCoverage]
    public class UpdateStockWorker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CheckAvailabilityWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UpdateStockWorker(IConfiguration configuration, ILogger<CheckAvailabilityWorker> logger, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceProvider = serviceProvider;

            //_consumerConfig = (ConsumerConfig?)_configuration.GetSection(nameof(ConsumerConfig));
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    var topicName = "CreatedOrder_Topic";
                    _logger.LogInformation("UpdateStockWorker running at: {time}", DateTimeOffset.Now);
                    await StartAsync(stoppingToken, topicName);

                }
                await Task.Delay(5000, stoppingToken);
            }
        }


        public async Task StartAsync(CancellationToken cancellationToken, string topicName)
        {
            _logger.LogInformation("Consumer UpdateStockWorkerworking..");

            try
            {                
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

                    var resultCreatedOrder = await eventConsumer.Consume<CreatedOrderEvent>(topicName, null);

                    if (resultCreatedOrder is null)
                    {
                        throw new ArgumentNullException("no se pudo procesar el mensaje");
                    }

                    using (IServiceScope scope2 = _serviceProvider.CreateScope())
                    {
                        eventConsumer = scope2.ServiceProvider.GetRequiredService<IEventConsumer>();

                        topicName = "StockRequest_Topic";
                        var resultUpdateStock = eventConsumer.Consume<UpdateStockRequestEvent>(topicName, resultCreatedOrder.OrderId.ToString());
                    }                    
                }

               
            }
            catch (Exception ex )
            {
                _logger.LogInformation("Error UpdateStockWorker working..");
                throw;
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer stopped..");
            return Task.CompletedTask;
        }
    }
}
