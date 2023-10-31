using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ModelVariantConfiguration : IEntityTypeConfiguration<ModelVariant>
    {
        public void Configure(EntityTypeBuilder<ModelVariant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ItemsLeft)
                   .IsRequired();
            builder.HasOne(x => x.Model)
                   .WithMany(x => x.Variants)
                   .HasForeignKey(x => x.ModelId);
            builder.HasOne(x => x.ModelSize)
                   .WithMany(x => x.Models)
                   .HasForeignKey(x => x.ModelSizeId);
        }
    }
}
