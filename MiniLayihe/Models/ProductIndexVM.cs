using System;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class ProductIndexVM
	{
		public List<Product>? Products { get; set; }
		public List<Category>? Categories { get; set; }
        public List<Brand>? Brands { get; set; }
        public List<Color>? Colors { get; set; }
		public List<ProductImage>? Image { get; set; }
	}
}

