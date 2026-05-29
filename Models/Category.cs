using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        // Mối quan hệ: Một chủ đề có nhiều sách
        public List<Book> Books { get; set; } = new List<Book>();
    }
}