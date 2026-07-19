using System;
using System.Linq;
using System.Web.Mvc;
using TeaShopWeb.Models;
using TeaShopWeb.Services;

namespace TeaShopWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController()
        {
            _productService = new ProductService();
        }

        // GET: /Cart/Index
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // POST: /Cart/AddToCart
        [HttpPost]
        public JsonResult AddToCart(int productId, int sizeId = 0, int quantity = 1)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });

            var cart = GetCart();
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId && i.SizeId == sizeId);
            if (existing != null)
                existing.Quantity += quantity;
            else
            {
                cart.Items.Add(new CartItemViewModel
                {
                    ProductId = productId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    SizeId = sizeId,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return Json(new { success = true, cartCount = cart.Items.Sum(i => i.Quantity) });
        }

        // POST: /Cart/UpdateCart
        [HttpPost]
        public JsonResult UpdateCart(int productId, int sizeId, int quantity)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId && i.SizeId == sizeId);
            if (item != null)
            {
                if (quantity <= 0)
                    cart.Items.Remove(item);
                else
                    item.Quantity = quantity;
                SaveCart(cart);
            }
            return Json(new { success = true, total = cart.TotalAmount, cartCount = cart.Items.Sum(i => i.Quantity) });
        }

        // POST: /Cart/RemoveFromCart
        [HttpPost]
        public JsonResult RemoveFromCart(int productId, int sizeId)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId && i.SizeId == sizeId);
            if (item != null)
            {
                cart.Items.Remove(item);
                SaveCart(cart);
            }
            return Json(new { success = true, total = cart.TotalAmount, cartCount = cart.Items.Sum(i => i.Quantity) });
        }

        // GET: /Cart/Checkout
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
                return RedirectToAction("Index", "Product");

            var order = new Order
            {
                TotalAmount = cart.TotalAmount,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };
            return View(order);
        }

        // POST: /Cart/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(Order order)
        {
            if (!ModelState.IsValid)
                return View("Checkout", order);

            var cart = GetCart();
            if (!cart.Items.Any())
                return RedirectToAction("Index", "Product");

            // Gán thêm thông tin (có thể lưu database sau)
            order.OrderDate = DateTime.Now;
            order.Status = "Đã tiếp nhận";
            order.University = "Đại học Trà Vinh";

            // Xóa giỏ hàng sau khi đặt
            Session.Remove("Cart");

            // Tạm thời chưa lưu DB, chuyển hướng thành công với id giả
            return RedirectToAction("Success", new { id = 1 });
        }

        // GET: /Cart/Success/1
        public ActionResult Success(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region Helpers
        private ShoppingCart GetCart()
        {
            var cart = Session["Cart"] as ShoppingCart;
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private void SaveCart(ShoppingCart cart)
        {
            Session["Cart"] = cart;
        }
        #endregion
    }
}