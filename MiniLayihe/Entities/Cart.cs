using System;
namespace MiniLayihe.Entities
{
	public class Cart
	{
		public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<Product>? Product { get; set; }
	}
}

