using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CartController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {


                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = _dbContext.Carts.FirstOrDefault(x => x.UserId == userId);

                var cart = _dbContext.Carts
                    .Include(x => x.CartItems)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                    .FirstOrDefault(x => x.UserId == userId);

                if (user == null) return View();

                var model = new CartIndexVM
                {
                    CartItems = cart.CartItems,
                    UserId = cart.UserId,
                    Products = cart.CartItems.Select(x => x.Product).ToList(),
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

    }
}

