namespace ShoesShop.WebApi.Dto
{
    public class ShoesSizeDto
    {
        /// <summary>
        /// Shoes size
        /// </summary>
        /// <example>42</example>
        public int Size { get; set; }
        /// <summary>
        /// Price of shoes model with size in roubles
        /// </summary>
        /// <example>1444</example>
        public decimal Price { get; set; }
        /// <summary>
        /// Number of left items of shoes model with size
        /// </summary>
        /// <example>3</example>
        public int ItemsLeft { get; set; }
    }
}
