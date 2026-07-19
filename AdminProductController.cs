using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeaShopWeb.Models;
using TeaShopWeb.Services;

namespace TeaShopWeb.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly IProductService _productService;

        public AdminProductController()
        {
            _productService = new ProductService();
        }

        // GET: /AdminProduct
        public ActionResult Index()
        {
            var products = _productService.GetAllProducts().ToList();
            return View(products);
        }

        // GET: /AdminProduct/Create
        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_productService.GetAllCategories(), "Id", "CategoryName");
            return View();
        }

        // POST: /AdminProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProduct(product, imageFile);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = new SelectList(_productService.GetAllCategories(), "Id", "CategoryName");
            return View(product);
        }

        // GET: /AdminProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var product = _productService.GetProductById(id.Value);
            if (product == null)
                return HttpNotFound();

            ViewBag.CategoryList = new SelectList(_productService.GetAllCategories(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: /AdminProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product, imageFile);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = new SelectList(_productService.GetAllCategories(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: /AdminProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var product = _productService.GetProductById(id.Value);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // POST: /AdminProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}