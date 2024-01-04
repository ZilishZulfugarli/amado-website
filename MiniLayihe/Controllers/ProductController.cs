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
        private readonly IShoppingCartService _shoppingCartService;

        public ProductController(AppDbContext dbContext, IShoppingCartService shoppingCartService)
		{
			_dbContext = dbContext;
			_shoppingCartService = shoppingCartService;
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

        //public IActionResult AddToCart(int id)
        //{
        //	var product = _dbContext.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

        //		var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //              _shoppingCartService.AddToCart(userId, product.Id, product.Name, product.Price, 1);

        //          var model = new Cart()
        //	{
        //		UserId = userId,
        //		ProductId = product.Id,
        //              ProductName = product.Name,
        //              Price = product.Price,
        //              Quantity = product.Quantity,
        //		Products = new List<Product> { product}
        //          };

        //          _dbContext.Carts.Add(model);
        //          _dbContext.SaveChanges();


        //          return RedirectToAction("Index", "Cart");
        //}

        public IActionResult AddToCart(int id, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _dbContext.Carts.Add(cart);
            }

            var product = _dbContext.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                // Handle the case where the product does not exist
                return NotFound();
            }

            // Check if the product is already in the cart
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == id);

            if (existingCartItem != null)
            {
                // Detach the existingCartItem from the context
                _dbContext.Entry(existingCartItem).State = EntityState.Detached;

                // Update the quantity of the existing item
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Add a new cart item
                var newCartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    Image = product.ProductImages?.FirstOrDefault(),
                    Product = product,
                };

                cart.CartItems.Add(newCartItem);
                _dbContext.CartItems.Add(newCartItem);
                _dbContext.Entry(newCartItem).State = EntityState.Added;
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

