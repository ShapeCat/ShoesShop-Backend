namespace ShoesShop.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid ModelId { get; set; }
        public bool IsPreview { get; set; }
        public string Url { get; set; }

        public Model Model { get; set; }
    }
}
