using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TeaShopWeb.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // 1. Tạo danh mục
            var categories = new List<Category>
            {
                new Category { CategoryName = "Trà sữa truyền thống", Description = "Vị trà đậm đà", IsActive = true },
                new Category { CategoryName = "Trà sữa đặc biệt", Description = "Kết hợp độc đáo", IsActive = true },
                new Category { CategoryName = "Topping", Description = "Các loại topping", IsActive = true }
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            // 2. Tạo sản phẩm mẫu
            var products = new List<Product>
            {
                new Product { ProductName = "Trà sữa thốt nốt truyền thống", Price = 30000, StockQuantity = 50, CategoryId = 1, ImageUrl = "/images/products/tra-sua-thot-not.jpg", Description = "Vị thốt nốt nguyên chất hòa quyện cùng trà sữa béo ngậy.", IsActive = true },
                new Product { ProductName = "Thốt nốt kem cheese", Price = 40000, StockQuantity = 30, CategoryId = 2, ImageUrl = "/images/products/kem-cheese.jpg", Description = "Kem cheese mặn béo phủ trên trà thốt nốt.", IsActive = true },
                new Product { ProductName = "Trà đào thốt nốt", Price = 35000, StockQuantity = 40, CategoryId = 1, ImageUrl = "/images/products/tra-dao-thot-not.jpg", Description = "Trà đào tươi mát kết hợp thốt nốt.", IsActive = true },
                new Product { ProductName = "Trân châu đen", Price = 5000, StockQuantity = 100, CategoryId = 3, ImageUrl = "/images/products/tran-chau-den.jpg", Description = "Trân châu đen dai giòn.", IsActive = true }
            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            // 3. Tạo tài khoản admin (nếu chưa có - cần cấu hình UserManager)
            // Phần này yêu cầu Identity đã cấu hình đầy đủ, tạm thời có thể thêm thủ công sau.
        }
    }
}