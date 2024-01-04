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
            

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Load user's cart with related products
            var user = _dbContext.Carts
                .Include(c => c.Products)
                .FirstOrDefault(x => x.UserId == userId);

            var users = _dbContext.Carts.Include(x => x.CartItems).FirstOrDefault(x => x.UserId == userId);

            var cart = _dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .ThenInclude(x => x.ProductImages)
            .FirstOrDefault(c => c.UserId == userId);
            if (user == null)
            {
                return View();
            }

            var model = new CartIndexVM
            {
                CartItems = cart.CartItems,
                Products = cart.CartItems.Select(ci => ci.Product).ToList(),
                UserId = cart.UserId,
                Price = cart.Products.Sum(p => p.Price),
                Quantity = cart.Quantity,
            };

            return View(model);
        }

    }
}

