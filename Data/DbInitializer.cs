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
             new Game{Name="Rust",Genre="Survival",Price=Decimal.Parse("30"),Logo="data:image/png;base64,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"},
             new Game{Name="Metin2",Genre="MMORPG",Price=Decimal.Parse("7"),Logo="https://styles.redditmedia.com/t5_2vvi6/styles/communityIcon_w23lhesv2y511.png"},
             new Game{Name="Minecraft",Genre="Sandbox",Price=Decimal.Parse("25"),Logo="https://seeklogo.com/images/M/minecraft-logo-5EAD3A1535-seeklogo.com.png"},
             new Game{Name="Dead by Daylight",Genre="Horror",Price=Decimal.Parse("11"),Logo="data:image/jpeg;base64,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"}
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
             new Order{GameID=1,PlayerID=50,OrderDate=DateTime.Parse("02-25-2021")},
             new Order{GameID=2,PlayerID=51,OrderDate=DateTime.Parse("07-21-2021")},
             new Order{GameID=3,PlayerID=50,OrderDate=DateTime.Parse("11-04-2021")},
             new Order{GameID=4,PlayerID=51,OrderDate=DateTime.Parse("12-16-2021")},
             new Order{GameID=5,PlayerID=50,OrderDate=DateTime.Parse("07-21-2021")},
             new Order{GameID=6,PlayerID=51,OrderDate=DateTime.Parse("10-24-2021")}

            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();
            var sellers = new Seller[]
             {
                 new Seller{SellerName="Valve",Logo="https://upload.wikimedia.org/wikipedia/commons/4/48/Valve_old_logo.svg"},
                 new Seller{SellerName="RiotGames",Logo="data:image/png;base64,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"},
                 new Seller{SellerName="Mojang",Logo="https://www.logolynx.com/images/logolynx/43/4367d3d3cbae6de3b20c6bf7584c40e7.jpeg"},
             };
            foreach (Seller s in sellers)
            {
                context.Sellers.Add(s);
            }
            context.SaveChanges();
            var listedGames = new ListedGame[]
             {
                 new ListedGame {
                 GameID = games.Single(c => c.Name == "Valorant" ).ID,
                 SellerID = sellers.Single(i => i.SellerName == "Valve").ID
                 },
                 new ListedGame {
                 GameID = games.Single(c => c.Name == "Dota2" ).ID,
                SellerID = sellers.Single(i => i.SellerName == "Valve").ID
                 },
                 new ListedGame {
                 GameID = games.Single(c => c.Name == "Rust" ).ID,
                 SellerID = sellers.Single(i => i.SellerName == "RiotGames").ID
                 },
                 new ListedGame {
                 GameID = games.Single(c => c.Name == "Metin2" ).ID,
                SellerID = sellers.Single(i => i.SellerName == "Mojang").ID
                 },
                 new ListedGame {
                 GameID = games.Single(c => c.Name == "Minecraft" ).ID,
                SellerID = sellers.Single(i => i.SellerName == "Mojang").ID
                 },
                 new ListedGame {
                 GameID = games.Single(c => c.Name == "Dead by Daylight" ).ID,
                 SellerID = sellers.Single(i => i.SellerName == "Mojang").ID
                 },
             };
            foreach (ListedGame lg in listedGames)
            {
                context.ListedGames.Add(lg);
            }
            context.SaveChanges();
        }
    }
}
