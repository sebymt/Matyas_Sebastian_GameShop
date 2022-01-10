using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Matyas_Sebastian_GameShop.Models
{
    public class Seller
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Seller Name")]
        [StringLength(50)]
        public string SellerName { get; set; }

        public string Logo { get; set; }
        public ICollection<ListedGame> ListedGames { get; set; }
    }
}
