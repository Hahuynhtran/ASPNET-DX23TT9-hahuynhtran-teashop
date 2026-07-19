using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShopWeb.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Người đặt")]
        public string UserId { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Tổng tiền")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Pending";

        [Required]
        [StringLength(500)]
        [Display(Name = "Địa chỉ giao hàng")]
        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        [Display(Name = "Ghi chú")]
        public string Notes { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Đơn vị quản lý")]
        public string University { get; set; } = "Đại học Trà Vinh";

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}