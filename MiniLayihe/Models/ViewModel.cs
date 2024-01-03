using System;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class ViewModel
	{
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public int Price { get; set; }
        public string? ColorName { get; set; }
        public string? Description { get; set; }
        public List<Brand>? Brands { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Color>? Colors { get; set; }
        public List<Product>? Products { get; set; }
    }
}

