using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BasePrice)
                   .IsRequired();
            builder.Property(x => x.Sale);
            builder.Property(x => x.SaleEndDate);
            builder.HasOne(x => x.ModelVariant)
                   .WithOne(x => x.Price)
                   .HasForeignKey<Price>(x => x.ModelVariantId);
        }
    }
}
