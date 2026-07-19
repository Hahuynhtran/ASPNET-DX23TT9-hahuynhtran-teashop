using System.Collections.Generic;
using System.Linq;

namespace TeaShopWeb.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<CartItemViewModel>();
        }

        public List<CartItemViewModel> Items { get; set; }

        public decimal TotalAmount
        {
            get { return Items.Sum(i => i.Price * i.Quantity); }
        }
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}