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
    public class AdminCategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        public AdminCategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var category = _dbContext.Categories.AsNoTracking().ToList();

            var model = new CategoryIndexVM()
            {
                Categories = category
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryAddVM model)
        {
            var category = new Category
            {
                Name = model.Name
            };

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();

            var model = new CategoryUpdateVM()
            {
                Name = category.Name
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdateVM model)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == model.Id);

            if (category is null) return NotFound();

            category.Name = model.Name;

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();

            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

