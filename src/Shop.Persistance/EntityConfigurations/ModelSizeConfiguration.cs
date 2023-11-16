using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ModelSizeConfiguration : IEntityTypeConfiguration<ModelSize>
    {
        public void Configure(EntityTypeBuilder<ModelSize> builder)
        {
            builder.ToTable("models_sizes");
            builder.HasKey(x => x.ModelSizeId);
            builder.Property(x => x.Size)
                   .IsRequired();
        }
    }
}
