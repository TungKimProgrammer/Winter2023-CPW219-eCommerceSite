using Microsoft.AspNetCore.Mvc;
using CPW219_eCommerceSite.Models;
using System.Security.Cryptography.Pkcs;
using CPW219_eCommerceSite.Data;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly GameContext _context;

        public MembersController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // Map RegisterViewModel data to Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel) 
        {
            if (ModelState.IsValid) 
            {
                // Check DB for credentials
                Member? m = await (from member in _context.Members
                                   where member.Email == loginModel.Email &&
                                         member.Password == loginModel.Password
                                   select member).SingleOrDefaultAsync();

                // If exist, send to home page
                if (m != null)
                {
                    HttpContext.Session.SetString("Email", loginModel.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found!");
            }

            // Return Page If no record found or ModelState is invalid
            return View(loginModel);
        }

    }
}
