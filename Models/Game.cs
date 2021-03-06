using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Matyas_Sebastian_GameShop.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public string Logo { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ListedGame> ListedGames { get; set; }
    }
}
    