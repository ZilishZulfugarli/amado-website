using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var products = _dbContext.Products.AsTracking().Include(x => x.ProductImages).Include(x => x.Brand).Include(x => x.Category).Include(x => x.Color).ToList();
            var colors = _dbContext.Colors.ToList();
            var brands = _dbContext.Brands.ToList();
            var categories = _dbContext.Categories.ToList();

            var model = new ViewModel()
            {
                Products = products,
                Colors = colors,
                Brands = brands,
                Categories = categories
            };
            return View(model);
        }
    }
}

