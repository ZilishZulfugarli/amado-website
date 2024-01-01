using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniLayihe.Entities;
using MiniLayihe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminPanelController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginIndexVM user)
        {
            if (ModelState.IsValid)
            {
                var model = await _userManager.FindByEmailAsync(user.Email);
                if (model is null) View(user);
                var result = await _signInManager.PasswordSignInAsync(model, user.Password, isPersistent: false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminHome", new {areas = "Admin"});
                }
            }
            return View(user);
        }
    }
}

