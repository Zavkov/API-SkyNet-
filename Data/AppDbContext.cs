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

            //Если в базе данных тип Price в сущности Priduct будет decimal то в данном случаи надо активировать данный кусок кода!
            //Что бы перезаписат тип поли в базе данных
            //if (Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
            //{
            //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //    {
            //        var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

            //        foreach (var property in properties)
            //        {
            //            modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(typeof(double));
            //        }
            //    }
            //}
        }
    }
}
