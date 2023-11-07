namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ShoesVm
    {
        /// <summary>
        /// Shoes ID
        /// </summary>
        /// <example>34c835b8-d5f1-499d-99ff-b8dc18b9bc3b</example>
        public Guid ShoesId { get; set; }
        /// <summary>
        /// Shoes name
        /// </summary>
        /// <example>Nike Light 2</example>
        public string Name { get; set; }
        /// <summary>
        /// Description ID
        /// </summary>
        /// <example>e8799dbc-cd52-4d77-855e-97db00fd21e4</example>
        public Guid? DescriptionId { get; set; }
        /// <summary>
        /// Shoes sizes IDs
        /// </summary>
        /// <example>faf484f9-c4a9-4ded-9072-ea692a3551a2</example>
        public ICollection<Guid>? SizesIds { get; set; }
    }
}
