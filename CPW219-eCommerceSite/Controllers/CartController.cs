using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly GameContext _context;
        private const string Cart = "ShoppingCart";

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

            CartGameViewModel cartGame = new()
            {
                GameId = gameToAdd.GameId,
                Title = gameToAdd.Title,
                Price = gameToAdd.Price
            };

            List<CartGameViewModel> cartGames = GetExistingCartData();

            cartGames.Add(cartGame);

            WriteShoppingCartCookie(cartGames);

            // Todo: Add item to shopping cart cookie
            TempData["Message"] = $"{cartGame.Title} added to cart!";
            return RedirectToAction("Index", "Games");
        }

        private void WriteShoppingCartCookie(List<CartGameViewModel> cartGames)
        {
            string cookieData = JsonConvert.SerializeObject(cartGames);

            // Serialization
            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
        }

        /// <summary>
        /// Return current list of video games in the users shopping cart cookie.
        /// If there is no cookie. an empty list will be returned
        /// </summary>
        /// <returns></returns>
        private List<CartGameViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return new List<CartGameViewModel>();
            }

            return JsonConvert.DeserializeObject<List<CartGameViewModel>>(cookie);
        }

        public IActionResult Summary()
        {
            // Read shopping cart data and convert to list of view model
            List <CartGameViewModel> cartGames = GetExistingCartData();

            return View(cartGames);
        }

        public IActionResult Remove(int id)
        {
            List<CartGameViewModel> cartGames = GetExistingCartData();
            CartGameViewModel? targetGame = 
                cartGames.Where(g => g.GameId == id).FirstOrDefault();
                //cartGames.FirstOrDefault(g => g.GameId == id);

            cartGames.Remove(targetGame);

            WriteShoppingCartCookie(cartGames);

            return RedirectToAction("Summary");
        }
    }
}
