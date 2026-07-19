using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TeaShopWeb.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mặc định cho University cho tất cả bảng
           

            // Cấu hình mối quan hệ
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<CartItem>()
                .HasRequired(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Product)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ProductId);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}