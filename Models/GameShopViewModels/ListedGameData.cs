using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matyas_Sebastian_GameShop.Models.GameShopViewModels
{
    public class ListedGameData
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
    }
}
