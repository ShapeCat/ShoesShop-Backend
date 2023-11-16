using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("images");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsPreview);
            builder.Property(x => x.Url)
                   .HasMaxLength(256)
                   .IsRequired();
            builder.HasOne(x => x.Model)
                   .WithMany(x => x.Images)
                   .HasForeignKey(x => x.ModelId);
        }
    }
}
