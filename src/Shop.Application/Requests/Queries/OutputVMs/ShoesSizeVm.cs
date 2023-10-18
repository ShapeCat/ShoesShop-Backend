namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ShoesSizeVm
    {
        /// <summary>
        /// Shoes size ID
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid SizeId { get; set; }
        /// <summary>
        /// Shoes ID
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid ShoesId { get; set; }
        /// <summary>
        /// Shoes size
        /// </summary>
        /// <example>39</example>
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
