using System;
namespace MiniLayihe.Entities
{
	public class Cart
	{
		public int Id { get; set; }
        public string? UserId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public List<Product>? Products { get; set; }
        public List<ProductImage>? Image { get; set; }
        public AppUser? User { get; set; }

        public decimal TotalPrice => CartItems.Sum(ci => ci.Price * ci.Quantity);
    }
}

