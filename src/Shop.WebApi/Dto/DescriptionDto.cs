using System.ComponentModel.DataAnnotations;

namespace ShoesShop.WebApi.Dto
{
    public class DescriptionDto
    {
        /// <summary>
        /// Shoes color
        /// Max Lenght: 255 chars
        /// </summary>
        /// <example>Purple</example>
        public string ColorName { get; set; }
        /// <summary>
        /// Unique product identifier  
        /// Max Lenght: 255 chars
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
