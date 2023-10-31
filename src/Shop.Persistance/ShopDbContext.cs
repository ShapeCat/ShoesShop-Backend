using Microsoft.EntityFrameworkCore;
using ShoesShop.Entities;
using ShoesShop.Persistence.EntityConfigurations;

namespace ShoesShop.Persistence
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Shoes> Shoes { get; set; } = null!;
        public DbSet<ShoesSize> Sizes { get; set; } = null!;
        public DbSet<Description> Descriptions { get; set; } = null!;

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShoesConfiguration());
            modelBuilder.ApplyConfiguration(new ShoesSizeConfiguration());
            modelBuilder.ApplyConfiguration(new DescriptionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
