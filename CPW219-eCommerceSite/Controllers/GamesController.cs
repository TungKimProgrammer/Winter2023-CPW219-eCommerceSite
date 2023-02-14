using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class GamesController : Controller
    {

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
