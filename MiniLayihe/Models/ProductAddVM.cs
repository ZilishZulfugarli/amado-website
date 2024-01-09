using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class ProductAddVM
	{
        [Required(ErrorMessage = "Please enter name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please enter quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please enter category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter color")]
        public int ColorId { get; set; }
        [Required(ErrorMessage = "Please enter brand")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Please enter image")]
        public List<IFormFile>? Image { get; set; }
        [Required(ErrorMessage = "Please enter category")]
        public Category? Categories { get; set; }
        public Color? Colors { get; set; }
        public Brand? Brands { get; set; }
        public List<SelectListItem>? Category { get; set; }
        public List<SelectListItem>? Brand { get; set; }
        public List<SelectListItem>? Color { get; set; }
    }
}
