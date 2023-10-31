using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ShoesConfiguration : IEntityTypeConfiguration<Shoes>
    {
        public void Configure(EntityTypeBuilder<Shoes> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnOrder(1);
            builder.HasOne(x => x.Description)
                   .WithOne(x => x.Shoes)
                   .HasForeignKey<Description>(x => x.ShoesId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Sizes)
                   .WithOne(x => x.Shoes)
                   .HasForeignKey(x => x.ShoesId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
