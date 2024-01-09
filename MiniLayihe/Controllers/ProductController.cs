using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;
using MiniLayihe.Services;

namespace MiniLayihe.Controllers
{
	public class ProductController : Controller
	{
        private readonly AppDbContext _dbContext;
        //private readonly IShoppingCartService _shoppingCartService;

        public ProductController(AppDbContext dbContext/*, IShoppingCartService shoppingCartService*/)
		{
			_dbContext = dbContext;
			//_shoppingCartService = shoppingCartService;
		}

		public IActionResult Index(int id)
        {
			var product = _dbContext.Products.Include(x => x.ProductImages).Include(x => x.Category).Include(x => x.Color).Include(x => x.Brand).FirstOrDefault(x => x.Id == id);

			var model = new ProductVM()
			{
				Products = product,
			};
			return View(model);
		}

        public IActionResult AddToCart(int id, int qty)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = _dbContext.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductImages)
                .FirstOrDefault(x => x.UserId == userId);

            if(cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _dbContext.Carts.Add(cart);
            }

            var product = _dbContext.Products
                .Include(x => x.ProductImages)
                .FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            var existingItem = _dbContext.Carts.FirstOrDefault(x => x.UserId == userId).CartItems.FirstOrDefault(x => x.ProductId == id);

            if(existingItem != null)
            {
                _dbContext.Entry(existingItem).State = EntityState.Detached;
            }
            else
            {
                var cartItem = new CartItem
                {
                    Cart = cart,
                    Product = product,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = qty
                };

                if(qty > product.Quantity)
                {
                    ModelState.AddModelError("Quantity", "Haven't enough product at stock!");
                    var model = new ProductVM()
                    {
                        Products = product,
                    };
                    return RedirectToAction("Index", "Product", model.Products.Id);
                }
                cart.CartItems.Add(cartItem);
                _dbContext.CartItems.Add(cartItem);
                _dbContext.Entry(cartItem).State = EntityState.Added;
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Delete(int id)
        {
            var cartItem = _dbContext.CartItems.FirstOrDefault(x => x.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _dbContext.CartItems.Remove(cartItem);

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }
}
}

