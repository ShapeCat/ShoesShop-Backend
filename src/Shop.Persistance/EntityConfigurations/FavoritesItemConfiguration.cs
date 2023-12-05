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
            builder.HasKey(x => x.FavoriteItemId);
            builder.HasOne(x => x.ModelVariant)
                   .WithMany(x => x.FavoritesIn)
                   .HasForeignKey(x => x.ModelVariantId);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Favorites)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
