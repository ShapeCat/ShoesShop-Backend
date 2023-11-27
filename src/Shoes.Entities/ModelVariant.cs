namespace ShoesShop.Entities
{
    public class ModelVariant
    {
        public Guid ModelVariantId { get; set; }
        public Guid ModelId { get; set; }
        public Guid ModelSizeId { get; set; }
        public int ItemsLeft { get; set; }
        public decimal Price { get; set; }

        public Model Model { get; }
        public ModelSize ModelSize { get; }
        public IEnumerable<Sale> Sales { get; }
        public ICollection<ShopCartItem> ShopCartsIn { get; }
        public ICollection<OrderItem> OrdersIn { get; }
        public ICollection<FavoritesItem> FavoritesIn { get; }

        public ModelVariant(Guid modelVariantId, Guid modelId, Guid modelSizeId, int itemsLeft, decimal price)
        {
            (ModelVariantId, ModelId, ModelSizeId, ItemsLeft, Price)
                = (modelVariantId, modelId, modelSizeId, itemsLeft, price);
        }

        public ModelVariant(Guid modelId, Guid modelSizeId, int itemsLeft, decimal price)
            : this(Guid.NewGuid(), modelId, modelSizeId, itemsLeft, price) { }
    }
}
