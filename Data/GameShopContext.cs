using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Matyas_Sebastian_GameShop.Models;

namespace Matyas_Sebastian_GameShop.Data
{
    public class GameShopContext: DbContext
    {
        public GameShopContext(DbContextOptions<GameShopContext> options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ListedGame> ListedGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<ListedGame>().ToTable("ListedGame");
            modelBuilder.Entity<ListedGame>().HasKey(c => new { c.GameID, c.SellerID }); //configureaza cheia primara compusa
        }
    }
}
 