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
            builder.HasKey(x => x.ShopCartItemId);
            builder.Property(x => x.Amount)
                   .IsRequired();
            builder.HasOne(x => x.ShopCart)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.ShopCartId);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.ShopCartsIn)
                   .HasForeignKey(x => x.ModeVariantId);

            builder.Navigation(x => x.ModelVariant)
                   .AutoInclude();
        }
    }
}
