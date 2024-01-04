using System;
namespace MiniLayihe.Entities
{
	public class ProductImage
	{
		public int Id { get; set; }
		public string? ImagePath { get; set; }
		public int ProductId { get; set; }
		public Product? Product { get; set; }
        public List<Cart>? Carts { get; set; }
    }
}

