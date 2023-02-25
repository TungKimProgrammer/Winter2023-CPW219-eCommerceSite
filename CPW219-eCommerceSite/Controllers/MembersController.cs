using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
