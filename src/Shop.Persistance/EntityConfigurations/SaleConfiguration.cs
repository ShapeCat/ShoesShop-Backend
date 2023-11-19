using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sales");
            builder.HasKey(x => x.SaleId);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.Sales)
                   .HasForeignKey(x => x.SaleId);
            builder.Property(x => x.Percent)
                   .IsRequired();
            builder.Property(x => x.SaleEndDate)
                   .IsRequired();
        }
    }
}
