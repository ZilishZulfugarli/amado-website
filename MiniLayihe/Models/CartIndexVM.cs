using System;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class CartIndexVM
	{
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<Product>? Products { get; set; }
	}
}

