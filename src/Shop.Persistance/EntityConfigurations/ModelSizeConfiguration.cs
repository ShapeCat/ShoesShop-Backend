using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ModelSizeConfiguration : IEntityTypeConfiguration<ModelSize>
    {
        public void Configure(EntityTypeBuilder<ModelSize> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Size)
                   .IsRequired();
        }
    }
}
