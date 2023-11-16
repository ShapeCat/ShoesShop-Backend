using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("orders_items");
            builder.HasKey(x => x.OrderItemId);
            builder.Property(x => x.Amount)
                   .IsRequired();
            builder.HasOne(x => x.Order)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.OrdersIn)
                   .HasForeignKey(x => x.ModelVariantId);

            builder.Navigation(x => x.ModelVariant)
                   .AutoInclude();
        }
    }
}
