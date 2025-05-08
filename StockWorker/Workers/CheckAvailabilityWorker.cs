using Confluent.Kafka;
using ExternalServices.Consumer;
using Microsoft.Extensions.Options;

namespace Worker.Workers
{
    public class CheckAvailabilityWorker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CheckAvailabilityWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CheckAvailabilityWorker(IConfiguration configuration, ILogger<CheckAvailabilityWorker> logger, IServiceProvider serviceProvider)
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
                    var topicName = "CheckAvailabilityRequest_Topic";
                    _logger.LogInformation("CheckAvailabilityWorker running at: {time}", DateTimeOffset.Now);
                    await StartAsync(stoppingToken, topicName);

                }
                await Task.Delay(3000, stoppingToken);
            }
        }


        public Task StartAsync(CancellationToken cancellationToken, string topicName)
        {
            _logger.LogInformation("Consumer working..");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

                var result = eventConsumer.Consume<bool>(topicName, null);

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
