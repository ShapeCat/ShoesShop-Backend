using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class FavoritesListConfiguration : IEntityTypeConfiguration<FavoritesList>
    {
        public void Configure(EntityTypeBuilder<FavoritesList> builder)
        {
            builder.ToTable("favorites_lists");
            builder.HasKey(x => x.FavoriteListId);
            builder.HasOne(x => x.Owner)
                   .WithOne(x => x.Favorites)
                   .HasForeignKey<FavoritesList>(x => x.UserId);

            builder.Navigation(x => x.Items)
                   .AutoInclude();
        }
    }
}
