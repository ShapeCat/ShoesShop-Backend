using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ShopCartConfiguration : IEntityTypeConfiguration<ShopCart>
    {
        public void Configure(EntityTypeBuilder<ShopCart> builder)
        {
            builder.ToTable("shop_carts");
            builder.HasKey(x => x.ShopCartId);
            builder.HasOne(x => x.Owner)
                   .WithMany(x => x.ShopCarts)
                   .HasForeignKey(x => x.UserId);

            builder.Navigation(x => x.Items)
                   .AutoInclude();
        }
    }
}
