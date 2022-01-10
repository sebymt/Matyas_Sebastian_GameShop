using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Matyas_Sebastian_GameShop.Models
{
    public class Player
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlayerID { get; set; }
        public string Nickname { get; set; }
        public string Rank { get; set; }
        public DateTime BirthDate { get; set; }
        public string Avatar { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
