using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;
using X.PagedList;

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
        public IActionResult Index(int productNumber,int pagenumber,int? categoryId, int? colorId, int? brandId)
        {
            if (pagenumber <= 0) pagenumber = 1;
            var defaultTake = 3;
            if (productNumber != 0)
            {
                defaultTake = productNumber;
            }
            var page = pagenumber;
            var colors = _dbContext.Colors.ToList();
            var brands = _dbContext.Brands.ToList();
            var categories = _dbContext.Categories.ToList();

            decimal productCount = _dbContext.Products.Count();
            var pageCount = (int)Math.Ceiling(productCount / defaultTake);

            var products = _dbContext.Products.AsTracking().Where(x =>
            (categoryId == null ? true : x.CategoryId == categoryId)
            && (brandId == null ? true : x.BrandId == brandId)
            && (colorId == null ? true : x.ColorId == colorId))
            .Include(x => x.ProductImages).ToList();

           
            var pagedProducts = products.Skip((page - 1)* defaultTake).Take(defaultTake).ToList();
            var model = new ViewModel()
            {
                Products = pagedProducts,
                PageCount = pageCount,
                CurrentPage = pagenumber,
                Colors = colors,
                Brands = brands,
                Categories = categories
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Search(string name)
        {
            var model = new ShopSearchVM();

            if (string.IsNullOrWhiteSpace(name))
            {
                model.Products = new List<Product>();
                return ViewComponent("SearchResult", model);
            }

            var products = _dbContext.Products
                .Include(x => x.Brand)
                .Include(x => x.ProductImages)
                .AsNoTracking()
                .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                .ToList();

            model.Products = products;

            return ViewComponent("SearchResult", model);
        }
    }
}

