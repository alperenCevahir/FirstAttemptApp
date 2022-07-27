using FirstAttempt.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FirstAttempt.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Best Practice açısından burayı kirletmiyoruz normalde, burdan da yapabildiğimizi
            //göstermek için burdan yaptık diğerleri için seed açtık
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                ProductId = 1,
                Width = 200,
            }, new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                ProductId = 2,
                Width = 200,
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
