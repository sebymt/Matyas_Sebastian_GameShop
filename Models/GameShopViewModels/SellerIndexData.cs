using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matyas_Sebastian_GameShop.Models.GameShopViewModels
{
    public class SellerIndexData
    {
        public IEnumerable<Seller> Sellers { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
