using Lab5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public double Price { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        // Khóa ngoại liên kết tới bảng Category
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}