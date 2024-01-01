using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class AdminColorController : Controller
    {
        private readonly AppDbContext _dbContext;
        public AdminColorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var color = _dbContext.Colors.AsNoTracking().ToList();

            var model = new ColorIndexVM()
            {
                Colors = color
            };
            return View(model);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Add(ColorAddVM color)
        {
            if (ModelState.IsValid)
            {
                var colors = new Color()
                {
                    Name = color.Name,
                    Code = color.Code
                };
                var sameColor = _dbContext.Colors.FirstOrDefault(x => x.Code == colors.Code);
                if (sameColor != null)
                {
                    ModelState.AddModelError(string.Empty, "This color has been added");

                    return RedirectToAction(nameof(Add));
                }

                _dbContext.Colors.Add(colors);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "AdminColor");
            }
            return View(color);
        }

        public IActionResult Update(int id)
        {
            if (id == null) return NotFound();

            var color = _dbContext.Colors.AsNoTracking().FirstOrDefault(x => x.Id == id);

            var model = new ColorUpdateVM()
            {
                Name = color.Name,
                Code = color.Code
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ColorUpdateVM model)
        {
            var colors = _dbContext.Colors.FirstOrDefault(x => x.Id == model.Id);

            if (colors == null) return NotFound();

            colors.Name = model.Name;
            colors.Code = model.Code;

            var sameColor = _dbContext.Colors.FirstOrDefault(x => x.Code == colors.Code);
            if (sameColor != null)
            {
                ModelState.AddModelError(string.Empty, "This color has been added");

                return RedirectToAction(nameof(Add));
            }

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var color = _dbContext.Colors.FirstOrDefault(x => x.Id == id);
            if (color == null) return View(color);

            _dbContext.Colors.Remove(color);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
}

