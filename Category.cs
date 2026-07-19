using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeaShopWeb.Models
{
    public class Category
    {
        // Khởi tạo collection trong constructor (tránh auto-initializer nếu bị cảnh báo)
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }  // bỏ dấu ?, cho phép null bằng cách không set giá trị mặc định

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; } = "Admin";

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Đơn vị quản lý")]
        public string University { get; set; } = "Đại học Trà Vinh";

        // Navigation Property
        public virtual ICollection<Product> Products { get; set; }
    }
}