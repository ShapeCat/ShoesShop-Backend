using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ShopCartConfiguration : IEntityTypeConfiguration<ShopCart>
    {
        public void Configure(EntityTypeBuilder<ShopCart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Owner)
                   .WithMany(x => x.ShopCarts)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
