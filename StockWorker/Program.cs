using Application.Service;
using Confluent.Kafka;
using Application.UseCases;
using ExternalServices.Consumer;
using Serilog;
using Worker.Workers;
using Application.UseCases.Interface;
using Application.Interface;
using Repository.Interface;
using Repository.Repositories;
using ExternalServices.KafkaConfig;
using Repository.Extensions;

namespace Worker
{
    public class Program
    {

        public static void Main(string[] args)
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
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
                services.AddCore();
                services.AddInfraestructure(hostContext.Configuration);

                services.Configure<KafkaSettings>
                (configuration.GetSection(nameof(KafkaSettings)));

                services.AddTransient<ICheckAvailabilityService, CheckAvailabilityService>();
                services.AddTransient<IUpdateStockService, UpdateStockService>();
                services.AddTransient<IIngredientRepository, IngredientRepository>();
                services.AddTransient<IRecipeRepository, RecipeRepository>();
                services.AddTransient<IStockUC, StockUC>();
                services.AddTransient<IEventConsumer, EventConsumer>();
                services.AddTransient<IProducer, Producer>();

                var provider = services.BuildServiceProvider();
                var test = provider.GetService<IEventConsumer>();
                Console.WriteLine(test != null ? "ICheckAvailabilityService registrado correctamente" : "ICheckAvailabilityService no está registrado");

                services.Configure<ConsumerConfig>(configuration.GetSection(nameof(ConsumerConfig)));

                services.AddHostedService<CheckAvailabilityWorker>();

            });

    }
}