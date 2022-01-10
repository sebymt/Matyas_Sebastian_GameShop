using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Matyas_Sebastian_GameShop.Data;
using Matyas_Sebastian_GameShop.Models;
using Matyas_Sebastian_GameShop.Models.GameShopViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Matyas_Sebastian_GameShop.Controllers
{
    [Authorize(Policy = "OnlySales")]
    public class SellersController : Controller
    {
        private readonly GameShopContext _context;

        public SellersController(GameShopContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index(int? id, int? gameID)
        {
            var viewModel = new SellerIndexData();
            viewModel.Sellers = await _context.Sellers
            .Include(i => i.ListedGames)
            .ThenInclude(i => i.Game)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Player)
            .AsNoTracking()
            .OrderBy(i => i.SellerName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["SellerID"] = id.Value;
                Seller seller = viewModel.Sellers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Games = seller.ListedGames.Select(s => s.Game);
            }
            if (gameID != null)
            {
                ViewData["GameID"] = gameID.Value;
                viewModel.Orders = viewModel.Games.Where(
                x => x.ID == gameID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SellerName,Logo")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var seller = await _context.Sellers
            .Include(i => i.ListedGames).ThenInclude(i => i.Game)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }
            PopulateListedGameData(seller);
            return View(seller);

        }
        private void PopulateListedGameData(Seller seller)
        {
            var allGames = _context.Games;
            var sellerGames = new HashSet<int>(seller.ListedGames.Select(c => c.GameID));
            var viewModel = new List<ListedGameData>();
            foreach (var game in allGames)
            {
                viewModel.Add(new ListedGameData
                {
                    GameID = game.ID,
                    Name = game.Name,
                    IsPublished = sellerGames.Contains(game.ID)
                });
            }
            ViewData["Games"] = viewModel;
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedGames)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sellerToUpdate = await _context.Sellers
            .Include(i => i.ListedGames)
            .ThenInclude(i => i.Game)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Seller>(
            sellerToUpdate,
            "",
            i => i.SellerName, i => i.Logo))
            {
                UpdateListedGames(selectedGames, sellerToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateListedGames(selectedGames, sellerToUpdate);
            PopulateListedGameData(sellerToUpdate);
            return View(sellerToUpdate);
        }
        private void UpdateListedGames(string[] selectedGames, Seller sellerToUpdate)
        {
            if (selectedGames == null)
            {
                sellerToUpdate.ListedGames = new List<ListedGame>();
                return;
            }
            var selectedGamesHS = new HashSet<string>(selectedGames);
            var listedGames = new HashSet<int>
            (sellerToUpdate.ListedGames.Select(c => c.Game.ID));
            foreach (var game in _context.Games)
            {
                if (selectedGamesHS.Contains(game.ID.ToString()))
                {
                    if (!listedGames.Contains(game.ID))
                    {
                        sellerToUpdate.ListedGames.Add(new ListedGame
                        {
                            SellerID =
                       sellerToUpdate.ID,
                            GameID = game.ID
                        });
                    }
                }
                else
                {
                    if (listedGames.Contains(game.ID))
                    {
                        ListedGame gameToRemove = sellerToUpdate.ListedGames.FirstOrDefault(i
                       => i.GameID == game.ID);
                        _context.Remove(gameToRemove);
                    }
                }
            }
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.ID == id);
        }
    }
}
