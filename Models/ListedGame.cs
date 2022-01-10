using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matyas_Sebastian_GameShop.Models
{
    public class ListedGame
    {
        public int SellerID { get; set; }
        public int GameID { get; set; }
        public Seller Seller { get; set; }
        public Game Game { get; set; }
    }
}
