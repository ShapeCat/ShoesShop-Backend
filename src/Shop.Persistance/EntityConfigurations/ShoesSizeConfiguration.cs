using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ShoesSizeConfiguration : IEntityTypeConfiguration<ShoesSize>
    {
        public void Configure(EntityTypeBuilder<ShoesSize> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Size)
                   .IsRequired()
                   .HasColumnOrder(1);
            builder.Property(x => x.Price)
                   .IsRequired()
                   .HasColumnOrder(2);
            builder.Property(x => x.ItemsLeft)
                   .HasColumnOrder(3);
        }
    }
}
