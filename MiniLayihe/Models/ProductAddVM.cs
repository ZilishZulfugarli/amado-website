using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class ProductAddVM
	{
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public List<IFormFile>? Image { get; set; }
        public Category? Categories { get; set; }
        public Color? Colors { get; set; }
        public Brand? Brands { get; set; }
        public List<SelectListItem>? Category { get; set; }
        public List<SelectListItem>? Brand { get; set; }
        public List<SelectListItem>? Color { get; set; }
    }
}
