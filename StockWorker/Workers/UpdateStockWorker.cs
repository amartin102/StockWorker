using Confluent.Kafka;
using ExternalServices.Consumer;
using Microsoft.Extensions.Options;

namespace Worker.Workers
{
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
                await Task.Delay(10000, stoppingToken);
            }
        }


        public Task StartAsync(CancellationToken cancellationToken, string topicName)
        {
            _logger.LogInformation("Consumer working..");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

                Task.Run(() => eventConsumer.Consume(topicName), cancellationToken);

            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer stopped..");
            return Task.CompletedTask;
        }
    }
}
