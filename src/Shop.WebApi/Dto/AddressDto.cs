﻿namespace ShoesShop.WebApi.Dto
{
    public class AddressDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }
}