using System.Linq;
using System.Web.Mvc;
using TeaShopWeb.Models;
using TeaShopWeb.Services;

namespace TeaShopWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        // GET: /Product/Index
        public ActionResult Index(string searchTerm = "", int? categoryId = null, string sortBy = "name")
        {
            var products = _productService.GetAllProducts().ToList();

            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(searchTerm.ToLower())).ToList();
                ViewBag.SearchTerm = searchTerm;
            }

            // Lọc theo danh mục
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
                ViewBag.SelectedCategory = categoryId.Value;
            }

            // Sắp xếp
            switch (sortBy)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName).ToList();
                    break;
            }
            ViewBag.SortBy = sortBy;

            // Lấy danh sách danh mục để hiển thị dropdown filter
            ViewBag.Categories = _productService.GetAllCategories();

            return View(products);
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var product = _productService.GetProductById(id.Value);
            if (product == null)
                return HttpNotFound();

            // Truyền danh sách size cho view
            ViewBag.Sizes = new SelectList(new[]
            {
                new { Id = 0, Name = "S - Nhỏ (không phụ phí)", Price = 0 },
                new { Id = 1, Name = "M - Vừa (+5,000đ)", Price = 5000 },
                new { Id = 2, Name = "L - Lớn (+10,000đ)", Price = 10000 }
            }, "Id", "Name");

            return View(product);
        }

        // GET: /Product/Search?keyword=abc
        public ActionResult Search(string keyword)
        {
            var products = _productService.SearchProducts(keyword);
            ViewBag.Keyword = keyword;
            return View("Index", products);
        }

        // GET: /Product/Category/5
        public ActionResult Category(int id)
        {
            var products = _productService.GetProductsByCategory(id);
            var category = _productService.GetAllCategories().FirstOrDefault(c => c.Id == id);
            ViewBag.CategoryName = category?.CategoryName ?? "Không xác định";
            return View("Index", products);
        }
    }
}