namespace ShoesShop.Entities
{
    public class FavoritesItem
    {
        public Guid FavoriteItemId { get; set; }
        public Guid UserId { get; set; }
        public Guid ModelVariantId { get; set; }

        public User User { get; }
        public ModelVariant ModelVariant { get; }

        public FavoritesItem(Guid favoriteItemId, Guid userId, Guid modelVariantId)
        {
            (UserId, ModelVariantId, FavoriteItemId)
                = (userId, modelVariantId, favoriteItemId);
        }

        public FavoritesItem(Guid favoritesListId, Guid modelVariantId)
        : this(Guid.NewGuid(), favoritesListId, modelVariantId) { }
    }
}
