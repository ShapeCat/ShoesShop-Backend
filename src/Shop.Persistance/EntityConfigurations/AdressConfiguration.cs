using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class AdressConfiguration : IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Country)
                   .HasMaxLength(128)
                   .IsRequired();
            builder.Property(x => x.City)
                   .HasMaxLength(128)
                   .IsRequired();
            builder.Property(x => x.Street)
                   .HasMaxLength(128)
                   .IsRequired();
            builder.Property(x => x.House)
                   .HasMaxLength(128)
                   .IsRequired();
            builder.Property(x => x.Room);
        }
    }
}
