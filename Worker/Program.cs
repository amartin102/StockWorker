using Confluent.Kafka;
using ExternalServices.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Extensions;
using Serilog;
using Worker.Workers;

namespace Worker
{
    public class Program
    {

        protected static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error("Fatal error ", ex);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>         
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) => {
                
                services.AddCore();
                services.AddInfraestructure(hostContext.Configuration);
                services.AddScoped<IEventConsumer, EventConsumer>();
                //services.Configure<ConsumerConfig>(Configuration.GetSection(nameof(ConsumerConfig)));

                services.AddHostedService<ConsumerHostedService>();
                services.AddHostedService<StockWorker>();

            });
    }
}
