using System;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class ShopSearchVM
	{
		public List<Product>? Products { get; set; }
		public List<Brand>? Brands { get; set; }
		public List<ProductImage>? Images { get; set; }
	}
}

