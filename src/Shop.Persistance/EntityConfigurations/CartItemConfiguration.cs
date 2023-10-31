using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount)
                   .IsRequired();
            builder.HasOne(x => x.ShopCart)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.ShopCartId);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.CartsIn)
                   .HasForeignKey(x => x.ModeVariantId);
        }
    }
}
