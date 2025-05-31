using Microsoft.EntityFrameworkCore;

using WaterShop.Goods.Domain.Entities;

namespace WaterShop.Goods.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBatch> ProductBatches { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
