using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Student
    {
        [Key] // Đánh dấu là Khoá chính
        public int StudentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ClassName { get; set; }

        public double AverageScore { get; set; }
    }
}