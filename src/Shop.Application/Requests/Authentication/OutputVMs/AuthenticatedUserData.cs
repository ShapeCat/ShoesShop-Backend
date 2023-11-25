﻿using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Authentication.OutputVMs
{
    public class AuthenticatedUserData
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Roles Role { get; set; }
    }
}
