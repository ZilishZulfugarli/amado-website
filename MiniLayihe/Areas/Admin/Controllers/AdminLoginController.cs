using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniLayihe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminLoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginIndexVM user)
        {
            if (ModelState.IsValid)
            {
                var model = await _userManager.FindByEmailAsync(user.Email);
                if (model is null) return View(user);
                var result = await _signInManager.PasswordSignInAsync(model, user.Password, isPersistent: false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Account locked out. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }

            return View(user);
        }
    }
}

