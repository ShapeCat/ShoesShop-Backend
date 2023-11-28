using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ShopCartItemConfiguration : IEntityTypeConfiguration<ShopCartItem>
    {
        public void Configure(EntityTypeBuilder<ShopCartItem> builder)
        {
            builder.ToTable("shop_carts_items");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Amount)
                   .IsRequired();
            builder.HasOne(x => x.User)
                   .WithMany(x => x.ShopCartItems)
                   .HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.ShopCartsItemsIn)
                   .HasForeignKey(x => x.ModeVariantId);

            builder.Navigation(x => x.ModelVariant)
                   .AutoInclude();
        }
    }
}
