using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("adresses");
            builder.HasKey(x => x.AddressId);
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
