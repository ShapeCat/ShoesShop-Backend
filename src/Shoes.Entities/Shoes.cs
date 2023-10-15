namespace ShoesShop.Entities
{
    public class Shoes : EntityAbstract
    {
        public string Name { get; set; }

        public Description? Description { get; set; }
        public ICollection<ShoesSize>? Sizes { get; set; }
    }
}