using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Repository.Coconseconsentext
{
    public class StockDb : DbContext
    {
        public string DefaultConnection { get; set; }

        public StockDb(DbContextOptions<StockDb> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            optionsBuilder.UseNpgsql(GetConnection());
            //}
        }

        private string GetConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            DefaultConnection = configuration.GetConnectionString("PostgreConnectionString");
            return DefaultConnection;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
              .HasMany(e => e.RecipeIngredients)
             .WithOne(ri => ri.Recipe)
             .HasForeignKey(ri => ri.IdRecipe)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeIngredient>()
          .HasOne(d => d.Recipe) 
          .WithMany(p => p.RecipeIngredients) 
          .HasForeignKey(d => d.IdRecipe) 
          .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<RecipeIngredient>()
           .HasOne(d => d.Ingredient) 
           .WithMany(p => p.Ingredients)
           .HasForeignKey(d => d.IdIngredient);

            modelBuilder.Entity<Ingredient>()
                 .HasOne(o => o.UnitOfMeasureIngredient) // Una orden tiene un cliente
                 .WithMany(c => c.Ingredients) // Un cliente tiene muchas órdenes
                 .HasForeignKey(o => o.IdUnitOfMeasure); // La clave foránea que referencia a Cliente

        }

        public DbSet<Recipe> Recipe { get; set; }

        public DbSet<Ingredient> Ingredient { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }

        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }


    }
}
