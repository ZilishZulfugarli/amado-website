using System;
using MiniLayihe.Entities;

namespace MiniLayihe.Services
{
    public interface IShoppingCartService
    {
        void AddToCart(int productId, string productName, decimal price, int quantity);
        List<Cart> GetCartItems();
    }
    public class ShoppingCartService : IShoppingCartService
	{
            private readonly List<Cart> _cart = new List<Cart>();

            public void AddToCart(int productId, string productName, decimal price, int quantity)
            {
                var existingItem = _cart.FirstOrDefault(item => item.ProductId == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var newItem = new Cart
                    {
                        ProductId = productId,
                        ProductName = productName,
                        Price = price,
                        Quantity = quantity
                    };

                    _cart.Add(newItem);
                }
            }

            public List<Cart> GetCartItems()
            {
                return _cart;
            }
        }
    }


