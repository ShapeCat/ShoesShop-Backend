namespace ShoesShop.Entities
{
    public class ShoesSize : EntityAbstract
    {
        public Guid ShoesId { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int ItemsLeft { get; set; }

        public Shoes? Shoes { get; set; }
    }
}
