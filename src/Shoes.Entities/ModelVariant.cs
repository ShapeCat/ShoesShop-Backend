namespace ShoesShop.Entities
{
    public class ModelVariant
    {
        public Guid ModelVariantId { get; set; }
        public Guid ModelId { get; set; }
        public Guid ModelSizeId { get; set; }
        public int ItemsLeft { get; set; }
        public decimal Price { get; set; }

        public Model Model { get; set; }
        public ModelSize ModelSize { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
        public ICollection<ShopcartItem> ShopcartsIn { get; set; }
        public ICollection<OrderItem> OrdersIn { get; set; }
        public ICollection<FavoritesItem> FavoritesIn { get; set; }
    }
}
