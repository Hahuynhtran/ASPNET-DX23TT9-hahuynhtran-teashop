using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShopWeb.Models
{
    public class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            CartItems = new HashSet<CartItem>();
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm tối đa 200 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Đường dẫn ảnh")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Số lượng tồn kho")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho phải lớn hơn hoặc bằng 0")]
        public int StockQuantity { get; set; } = 0;

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Đơn vị quản lý")]
        public string University { get; set; } = "Đại học Trà Vinh";

        // Navigation Properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}