using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly GameContext _context;

        public CartController(GameContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Add(int id)
        {
            Game? gameToAdd = await _context.Games.Where(g => g.GameId == id).SingleOrDefaultAsync(); 
            
            if (gameToAdd == null)
            {
                // Game with specified id does not exists
                TempData["Message"] = "That game no longer exists!";
                return RedirectToAction("Index", "Games");
            }

            // Todo: Add item to shopping cart cookie
            TempData["Message"] = "Item added to cart!";
            return RedirectToAction("Index", "Games");
        }
    }
}
