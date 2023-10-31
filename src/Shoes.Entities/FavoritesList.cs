namespace ShoesShop.Entities
{
    public class FavoritesList
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public ICollection<FavoritesItem> Items { get; set; }
        public User Owner { get; set; }
    }
}
