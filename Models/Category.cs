using System.ComponentModel.DataAnnotations;

namespace BaiTapBuoi6.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Mã danh mục")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        // Quan hệ 1-n: Một danh mục có nhiều sản phẩm
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
