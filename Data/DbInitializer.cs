using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matyas_Sebastian_GameShop.Models;

namespace Matyas_Sebastian_GameShop.Data
{
    public class DbInitializer
    {
        public static void Initialize(GameShopContext context)
        {
            context.Database.EnsureCreated();
            if (context.Games.Any())
            {
                return; // BD a fost creata anterior
            }
            var games = new Game[]
            {
             new Game{Name="Valorant",Genre="FPS",Price=Decimal.Parse("5"),Logo="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0RxonCdPCT2BiRDd06atxoDkk5DFBLJEPez8oAAeB13swAGfc9cZYupqaicAwEYJvkw4&usqp=CAU"},
             new Game{Name="Dota2",Genre="Moba",Price=Decimal.Parse("10"),Logo="https://www.meme-arsenal.com/memes/520e1b3eaa2efdb2fe341e58ae62a394.jpg"},
             new Game{Name="Rust",Genre="Survival",Price=Decimal.Parse("30"),Logo="data:image/png;base64,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"}
            };
            foreach (Game g in games)
            {
                context.Games.Add(g);
            }
            context.SaveChanges();
            var players = new Player[]
            {
             new Player{PlayerID=50,Nickname="ScreaM",Rank="diamond",BirthDate=DateTime.Parse("1989-09-01"),Avatar="test"},
             new Player{PlayerID=51,Nickname="KennyS",Rank="platinum",BirthDate=DateTime.Parse("1992-07-08"),Avatar="test1"},
            };
            foreach (Player p in players)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
             new Order{GameID=1,PlayerID=50},
             new Order{GameID=3,PlayerID=51},
             new Order{GameID=1,PlayerID=50},
             new Order{GameID=2,PlayerID=51},
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();
        }
    }
}
