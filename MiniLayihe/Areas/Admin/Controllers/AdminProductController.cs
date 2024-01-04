using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniLayihe.Data;
using MiniLayihe.Entities;
using MiniLayihe.Models;
using MiniLayihe.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class AdminProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly FileService _fileService;

        public AdminProductController(AppDbContext dbContext, FileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var products = _dbContext.Products.AsTracking().Include(x => x.ProductImages).Include(x => x.Category).Include(x => x.Color).Include(x => x.Brand).ToList();

            var model = new ProductIndexVM()
            {
                Products = products
            };
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new ProductAddVM();

            var categories = _dbContext.Categories.ToList();
            var brands = _dbContext.Brands.ToList();
            var colors = _dbContext.Colors.ToList();

            model.Brand = brands.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Color = colors.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Category = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ProductAddVM model)
        {
            if (!ModelState.IsValid)
            {
                var categories = _dbContext.Categories.ToList();
                var brands = _dbContext.Brands.ToList();
                var colors = _dbContext.Colors.ToList();

                model.Brand = brands.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                model.Color = colors.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                model.Category = categories.Select(x=> new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();


                return View(model);
            }

            var productImage = new List<ProductImage>();
            foreach (var images in model.Image)
            {
                var imagePath = _fileService.UploadFile(images);

                productImage.Add(new ProductImage
                {
                    ImagePath = imagePath
                });
            }

            //var image = _fileService.UploadFile(model.Image);

            var product = new Product
            {
                Name = model.Name,
                Price = (decimal)model.Price,
                Description = model.Description,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                ColorId = model.ColorId,
                Quantity = model.Quantity,
                ProductImages = productImage
            };

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var product = _dbContext.Products.Include(x => x.Category).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            var categories = _dbContext.Categories.ToList();
            var brands = _dbContext.Brands.ToList();
            var colors = _dbContext.Colors.ToList();

            var model = new ProductUpdateVM
            {
                BrandId = (int)product.BrandId,
                CategoryId = (int)product.CategoryId,
                ColorId = (int)product.ColorId,
                Price = (int)product.Price,
                Name = product.Name,
                Description = product.Description,
                ImageName = product.ProductImages?.FirstOrDefault()?.ImagePath ?? string.Empty
            };

            
            model.Brand = brands.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Color = colors.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Category = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateVM model)
        {
            var product = _dbContext.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == model.Id);

            if (product == null) return NotFound();

            product.Name = model.Name;
            product.BrandId = model.BrandId;
            product.CategoryId = model.CategoryId;
            product.ColorId = model.ColorId;
            product.Price = model.Price;
            product.Description = model.Description;

            
            if(model.Image != null)
            {
                if(product.ProductImages != null)
                {
                    foreach(var img in product.ProductImages)
                    {
                        _fileService.DeleteFile(img.ImagePath);
                    }
                }
            }

            var image = _fileService.UploadFile(model.Image);
            product.ProductImages = new List<ProductImage>
                {
                   new ProductImage
                   {
                       ImagePath = image
                   }
                };

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            _dbContext.Products.Remove(product);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}