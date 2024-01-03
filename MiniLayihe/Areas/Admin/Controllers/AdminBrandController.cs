using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBrandController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AdminBrandController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var brand = _dbContext.Brands.AsTracking().ToList();

            var model = new BrandIndexVM
            {
                Brands = brand
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BrandAddVM model)
        {
            var brand = new Brand
            {
                Name = model.Name
            };

            _dbContext.Brands.Add(brand);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);
            if(brand is null) return NotFound();

            var model = new BrandUpdateVM()
            {
                Name = brand.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(BrandUpdateVM model)
        {
            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == model.Id);
            if(brand is null) return NotFound();

            brand.Name = model.Name;

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);

            if (brand == null) return NotFound();

            _dbContext.Brands.Remove(brand);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

