namespace ShoesShop.Entities
{
    public class Model
    {
        public Guid ModelId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brend { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }

        public ICollection<Image> Images { get; set; }
        public ICollection<Review> Rewiews { get; set; }
        public ICollection<ModelVariant> Variants { get; set; }
    }
}