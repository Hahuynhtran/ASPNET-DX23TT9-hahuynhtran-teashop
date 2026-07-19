using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShopWeb.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(1, 5)]
        [Display(Name = "Đánh giá (1-5 sao)")]
        public int Rating { get; set; }

        [Display(Name = "Bình luận")]
        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Đã duyệt")]
        public bool IsApproved { get; set; } = false;

        [Display(Name = "Đơn vị quản lý")]
        public string University { get; set; } = "Đại học Trà Vinh";

        // Navigation Properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}