namespace ShoesShop.Entities
{
    public class ModelSize
    {
        public Guid Id { get; set; }
        public int Size { get; set; }

        public ICollection<ModelVariant> Models { get; set; }
    }
}
