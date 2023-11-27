namespace ShoesShop.Entities
{
    public class FavoritesItem
    {
        public Guid FavoriteItemId { get; set; }
        public Guid FavoritesListId { get; set; }
        public Guid ModelVariantId { get; set; }

        public FavoritesList FavoritesList { get; }
        public ModelVariant ModelVariant { get; }

        public FavoritesItem(Guid favoriteItemId, Guid favoritesListId, Guid modelVariantId)
        {
            (FavoritesListId, ModelVariantId, FavoriteItemId)
                = (favoritesListId, modelVariantId, favoriteItemId);
        }

        public FavoritesItem(Guid favoritesListId, Guid modelVariantId)
        : this(Guid.NewGuid(), favoritesListId, modelVariantId) { }
    }
}
