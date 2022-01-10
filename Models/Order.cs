using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matyas_Sebastian_GameShop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int PlayerID { get; set; }
        public int GameID { get; set; }
        public DateTime OrderDate { get; set; }
        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}
