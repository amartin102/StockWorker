using Application.Interface;
using Application.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Coconseconsentext;
using Repository.Interface;
using Repository.Repositories;

namespace StockApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<StockDb>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("PostgreConnectionString"));
            });

            services.AddTransient<ICheckAvailabilityService, CheckAvailabilityService>();
            services.AddTransient<IUpdateStockService, UpdateStockService>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();                   

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
