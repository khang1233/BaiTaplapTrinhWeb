using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTapBuoi6.Models
{
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã ảnh")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        [Display(Name = "Đường dẫn hình ảnh")]
        public string Url { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sản phẩm là bắt buộc")]
        [Display(Name = "Mã sản phẩm")]
        public int ProductId { get; set; }

        // Thuộc tính điều hướng đến Product
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
