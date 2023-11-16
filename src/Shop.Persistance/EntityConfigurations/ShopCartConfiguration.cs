using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ShopcartConfiguration : IEntityTypeConfiguration<Shopcart>
    {
        public void Configure(EntityTypeBuilder<Shopcart> builder)
        {
            builder.ToTable("shopcarts");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Owner)
                   .WithMany(x => x.Shopcarts)
                   .HasForeignKey(x => x.UserId);

            builder.Navigation(x => x.Items)
                   .AutoInclude();
        }
    }
}
