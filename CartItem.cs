using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShopWeb.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? SizeId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;

        [Display(Name = "Đơn vị quản lý")]
        public string University { get; set; } = "Đại học Trà Vinh";

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }
    }
}