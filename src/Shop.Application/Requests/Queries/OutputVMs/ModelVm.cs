﻿namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ModelVm
    {
        public Guid ModelId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brend { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<ImageVm> Images { get; set; }
    }
}