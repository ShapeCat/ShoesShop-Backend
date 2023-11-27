namespace ShoesShop.Entities
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public Guid ModelId { get; set; }
        public bool IsPreview { get; set; }
        public string Url { get; set; }

        public Model Model { get; }

        public Image(Guid imageId, Guid modelId, bool isPreview, string url)
        {
            (ModelId, Url, IsPreview, ImageId)
                = (modelId, url, isPreview, imageId);
        }

        public Image(Guid modelId, string url, bool isPreview = false)
           : this(Guid.NewGuid(), modelId, isPreview, url) { }
    }
}
