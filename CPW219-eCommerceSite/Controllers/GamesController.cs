using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        // DO NOT REASSIGN THIS 
        private readonly GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Game g)
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                _context.Games.Add(g); // Prepares insert
                _context.SaveChanges(); // Executes pending statements

                // Show success message on page
                ViewData["Message"] = $"{g.Title} was added successfully!";
                return View();
            }

            return View(g);
        }
    }
}
