using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ShopcartItemConfiguration : IEntityTypeConfiguration<ShopcartItem>
    {
        public void Configure(EntityTypeBuilder<ShopcartItem> builder)
        {
            builder.ToTable("shopcarts_items");
            builder.HasKey(x => x.ShopcartItemId);
            builder.Property(x => x.Amount)
                   .IsRequired();
            builder.HasOne(x => x.Shopcart)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.ShopcartId);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.ShopcartsIn)
                   .HasForeignKey(x => x.ModeVariantId);

            builder.Navigation(x => x.ModelVariant)
                   .AutoInclude();
        }
    }
}
