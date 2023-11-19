using Microsoft.EntityFrameworkCore;
using ShoesShop.Entities;
using ShoesShop.Persistence.EntityConfigurations;

namespace ShoesShop.Persistence
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<ShopcartItem> ShopcartsItems { get; set; } = null!;
        public DbSet<FavoritesItem> FavoritesItems { get; set; } = null!;
        public DbSet<FavoritesList> FavoritesLists { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;
        public DbSet<ModelSize> ModelsSizes { get; set; } = null!;
        public DbSet<ModelVariant> ModelsVariants { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrdersItems { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Shopcart> Shopcarts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new ShopcartItemConfiguration());
            modelBuilder.ApplyConfiguration(new FavoritesItemConfiguration());
            modelBuilder.ApplyConfiguration(new FavoritesListConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
            modelBuilder.ApplyConfiguration(new ModelSizeConfiguration());
            modelBuilder.ApplyConfiguration(new ModelVariantConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new ShopcartConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
