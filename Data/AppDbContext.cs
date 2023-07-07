using Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            ////Database.Migrate();
        }
        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductType> ProductTypes { get; set; } 
        public DbSet<ProductBrand> ProductBrands { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
