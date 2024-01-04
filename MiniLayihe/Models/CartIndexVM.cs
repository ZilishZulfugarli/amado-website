using System;
using MiniLayihe.Entities;

namespace MiniLayihe.Models
{
	public class CartIndexVM
	{
        public string? UserId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public AppUser? User { get; set; }
        public List<Product>? Products { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}

