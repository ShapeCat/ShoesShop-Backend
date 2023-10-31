using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Rating)
                   .IsRequired();
            builder.Property(x => x.Comment);
            builder.Property(x => x.PublishDate)
                   .IsRequired();
            builder.HasOne(x => x.Model)
                   .WithMany(x => x.Rewiews)
                   .HasForeignKey(x=>x.ModelId);
            builder.HasOne(x => x.Author)
                   .WithMany(x => x.Rewiews)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
