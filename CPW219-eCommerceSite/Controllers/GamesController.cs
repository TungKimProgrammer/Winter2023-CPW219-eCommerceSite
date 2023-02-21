using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            // Get all games from the DB
            List<Game> games = _context.Games.ToList();
            List<Game> games2 = await (from game in _context.Games
                                       select game).ToListAsync();

            // Show them on the page

            return View(games2);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game g)
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                _context.Games.Add(g);              // Prepares insert
                await _context.SaveChangesAsync();  // Executes pending statements

                // Show success message on page
                ViewData["Message"] = $"{g.Title} was added successfully!";
                return View();
            }

            return View(g);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Game? gameToEdit = await _context.Games.FindAsync(id);

            if (gameToEdit == null)
            {
                return NotFound();
            } 

            return View(gameToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Game gameModel)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Update(gameModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{gameModel.Title} was updated successfully!";
                return RedirectToAction("Index");
            }

            return View(gameModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete == null)
            {
                return NotFound();
            }

            return View(gameToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete != null) 
            {
                _context.Games.Remove(gameToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{gameToDelete.Title} was deleted successfully!";
                
                return RedirectToAction("Index");
            }
            TempData["Message"] = $"This game was already deleted!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Game? gameDetail = await _context.Games.FindAsync(id);

            if (gameDetail == null)
            {
                return NotFound();
            }

            return View(gameDetail);
        }
    }
}
