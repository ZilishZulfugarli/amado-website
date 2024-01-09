using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLoginController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public AdminLoginController(AppDbContext dbContext,UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Show(AdminAccountEditorVM emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress.Mail);
            if(user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDetail = new UserDetailVM()
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Roles = roles.ToList()
                };

                return View("Show", userDetail);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found for the provided email address.");
            }

            return View("Index", emailAddress);
        }

        public async Task<IActionResult> GiveAdmin(string mail) 
        {
            var user = await _userManager.FindByEmailAsync(mail);

            await _userManager.AddToRoleAsync(user, "Admin");

            _dbContext.SaveChanges();

            return View("Index");
        }
        public async Task<IActionResult> DeleteAdmin(string mail)
        {
            var user = await _userManager.FindByEmailAsync(mail);

            await _userManager.RemoveFromRoleAsync(user, "Admin");

            _dbContext.SaveChanges();

            return View("Index");
        }
    }
}

