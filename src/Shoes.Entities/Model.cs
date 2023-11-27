namespace ShoesShop.Entities
{
    public class Model
    {
        public Guid ModelId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }

        public ICollection<Image> Images { get; }
        public ICollection<Review> Reviews { get; }
        public ICollection<ModelVariant> Variants { get; }


        public Model(Guid modelId, string name, string color, string brand, string skuId, DateTime releaseDate)
        {
            (ModelId, Name, Color, Brand, SkuId, ReleaseDate)
                = (modelId, name, color, brand, skuId, releaseDate);
        }

        public Model(string name, string color, string brand, string skuId, DateTime releaseDate)
            : this(Guid.NewGuid(), name, color, brand, skuId, releaseDate) { }
    }
}