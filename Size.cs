using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeaShopWeb.Models
{
    public class Size
    {
        public Size()
        {
            OrderDetails = new HashSet<OrderDetail>();
            CartItems = new HashSet<CartItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên size")]
        public string SizeName { get; set; }

        [Display(Name = "Giá cộng thêm")]
        [Range(0, double.MaxValue)]
        public decimal PriceModifier { get; set; } = 0;

        [StringLength(200)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Đơn vị quản lý")]
        public string University { get; set; } = "Đại học Trà Vinh";

        // Navigation Properties
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}