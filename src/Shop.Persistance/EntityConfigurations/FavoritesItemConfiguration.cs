using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class FavoritesItemConfiguration : IEntityTypeConfiguration<FavoritesItem>
    {
        public void Configure(EntityTypeBuilder<FavoritesItem> builder)
        {
            builder.ToTable("favorites_items");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.FavoritesIn)
                   .HasForeignKey(x => x.Id);
            builder.HasOne(x => x.FavoritesList)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.FavoritesListId);

            builder.Navigation(x => x.ModelVariant)
                   .AutoInclude();
        }
    }
}
