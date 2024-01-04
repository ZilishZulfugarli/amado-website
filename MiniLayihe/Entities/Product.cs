using System;
namespace MiniLayihe.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
		public string? Description { get; set; }
		public int? CategoryId { get; set; }
		public int? ColorId { get; set; }
		public int? BrandId { get; set; }
		public int Quantity { get; set; }
		public List<Cart>? Carts { get; set; }
		public Category? Category { get; set; }
		public Color? Color { get; set; }
		public Brand? Brand { get; set; }
		public List<ProductImage>? ProductImages { get; set; }

    }
}

