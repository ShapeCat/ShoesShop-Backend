using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status)
                   .IsRequired();
            builder.Property(x => x.CreationDate);
            builder.HasOne(x => x.Owner)
                   .WithMany(x => x.Orders)
                   .HasForeignKey(x => x.UserId);

            builder.Navigation(x => x.Items)
                   .AutoInclude();
        }
    }
}
