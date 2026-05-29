using Microsoft.EntityFrameworkCore;

namespace Lab5.Models
{
    public class MyDbContext : DbContext
    {
        // Hàm khởi tạo nhận cấu hình chuỗi kết nối từ hệ thống
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        // Khai báo bảng Students trong cơ sở dữ liệu
        public DbSet<Student> Students { get; set; }

        // ==========================================
        // BƯỚC THÊM DỮ LIỆU MẪU (SEED DATA)
        // ==========================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình nạp sẵn dữ liệu mẫu khi tạo bảng
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentID = 1, Name = "Tran Minh Khang", ClassName = "CC01", AverageScore = 8.5 },
                new Student { StudentID = 2, Name = "Le Van Khang", ClassName = "CC01", AverageScore = 7.9 },
                new Student { StudentID = 3, Name = "Tran Thi Hoa", ClassName = "CC02", AverageScore = 9.0 }
            );
        }
    }
}