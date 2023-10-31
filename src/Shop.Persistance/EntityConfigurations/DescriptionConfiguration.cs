using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class DescriptionConfiguration : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SkuID)
                   .IsRequired()
                   .HasColumnOrder(1);
            builder.Property(x => x.ReleaseDate)
                   .IsRequired()
                   .HasColumnOrder(2);
            builder.Property(x => x.ColorName)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnOrder(3)
;
        }
    }
}
