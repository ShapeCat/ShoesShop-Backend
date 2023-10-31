namespace ShoesShop.Entities
{
    public class FavoritesItem
    {
        public Guid Id { get; set; }
        public Guid FavoritesListId { get; set; }
        public Guid ModelVariantId { get; set; }

        public FavoritesList FavoritesList { get; set; }
        public ModelVariant ModelVariant { get; set; }
    }
}
