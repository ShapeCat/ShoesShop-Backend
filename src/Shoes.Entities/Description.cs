namespace ShoesShop.Entities
{
    public class Description : EntityAbstract
    {
        public Guid ShoesId { get; set; }
        public string SkuID { get; set; }
        public string ColorName { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Shoes Shoes { get; set; }
    }
}
