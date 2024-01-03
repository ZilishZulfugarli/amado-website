using System;
using Microsoft.AspNetCore.Mvc;
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
				Products = product
			};
			return View(model);
		}

		public IActionResult AddToCart(int id)
		{
			var product = _dbContext.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                _shoppingCartService.AddToCart(product.Id, product.Name, product.Price, 1);
            }

            return RedirectToAction("Index", "Product");
        }
	}
}

