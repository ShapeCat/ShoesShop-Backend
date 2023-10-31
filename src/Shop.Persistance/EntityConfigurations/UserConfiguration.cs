using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.UserName)
                   .HasMaxLength(128)
                   .IsRequired();
            builder.Property(x => x.Login)
                   .HasMaxLength(128)
                   .IsRequired();
            builder.Property(x => x.Password)
                   .HasMaxLength(256)
                   .IsRequired();
            builder.Property(x => x.Phone)
                   .HasMaxLength(50);
            builder.HasOne(x => x.Adress)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x =>x.AdressId);
        }
    }
}
