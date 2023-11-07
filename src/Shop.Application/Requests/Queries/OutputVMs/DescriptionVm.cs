namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class DescriptionVm
    {
        /// <summary>
        /// Description ID
        /// </summary>
        /// <example>b660b250-a586-41cf-b534-01a773fba818</example>
        public Guid DescriptionId { get; set; }
        /// <summary>
        /// Shoes ID
        /// </summary>
        /// <example>9317761b-33d7-43d8-b7d1-643261200517</example>
        public Guid ShoesId { get; set; }
        /// <summary>
        /// Shoes color
        /// </summary>
        /// <example>Purple</example>
        public string ColorName { get; set; }
        /// <summary>
        /// Unique product identifier  
        /// </summary>
        /// <example>Shoes_white_44</example>
        public string SkuID { get; set; }
        /// <summary>
        /// Date of release in  ISO 8601 format
        /// </summary>
        /// <example>2023-10-17T21:44:16.871Z</example>
        public DateTime ReleaseDate { get; set; }
    }
}
