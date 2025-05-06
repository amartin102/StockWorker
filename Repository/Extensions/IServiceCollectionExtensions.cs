using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Coconseconsentext;
using AutoMapper;

namespace Repository.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services) {
            return services;
        }

        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration) {
            
            services.AddDbContext<StockDb>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreConnectionString"));
                options.ConfigureWarnings(warnings =>
                {
                    warnings.Default(WarningBehavior.Log);
                });

            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            //services.AddScoped<>();

            return services;

        }
    }
}
