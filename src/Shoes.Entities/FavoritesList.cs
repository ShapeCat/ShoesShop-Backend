//namespace ShoesShop.Entities
//{
//    public class FavoritesList
//    {
//        public Guid FavoriteListId { get; set; }
//        public Guid UserId { get; set; }

//        public ICollection<FavoritesItem> Items { get; }
//        public User Owner { get; }

//        public FavoritesList(Guid favoritesListId, Guid userId)
//        {
//            (FavoriteListId, UserId)
//                = (favoritesListId, userId);
//        }

//        public FavoritesList(Guid userId)
//            : this(Guid.NewGuid(), userId) { }
//    }
//}
