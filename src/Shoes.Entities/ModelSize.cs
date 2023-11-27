namespace ShoesShop.Entities
{
    public class ModelSize
    {
        public Guid ModelSizeId { get; set; }
        public int Size { get; set; }

        public ICollection<ModelVariant> Models { get; }


        public ModelSize(Guid modelSizeId, int size)
        {
            (ModelSizeId, Size)
               = (modelSizeId, size);
        }

        public ModelSize(int size)
            : this(Guid.NewGuid(), size) { }
    }
}
