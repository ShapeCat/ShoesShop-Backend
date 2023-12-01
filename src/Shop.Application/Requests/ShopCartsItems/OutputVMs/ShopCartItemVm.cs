using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Application.Requests.ShopCartsItems.OutputVMs
{
    public class ShopCartItemVm
    {
        public Guid ShopCartItemId { get; set; }
        public Guid ModelVariantId { get; set; }
        public int Amount { get; set; }
    }
}
