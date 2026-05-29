using Lab5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Models
{
    public class BookDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Thực hiện chèn dữ liệu mẫu ban đầu (Seed Data) theo yêu cầu
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Thêm dữ liệu mẫu cho bảng Chủ đề (Category)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Truyện Tranh" },
                new Category { CategoryId = 2, CategoryName = "Tiểu Thuyết" },
                new Category { CategoryId = 3, CategoryName = "Khoa Học" }
            );

            // 2. Thêm dữ liệu mẫu cho bảng Sách (Book) đúng liên kết khóa ngoại
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "One Piece Tập 100", Author = "Eiichiro Oda", Price = 25, ImageUrl = "onepiece.jpg", CategoryId = 1 },
                new Book { BookId = 2, Title = "Thám Tử Lừng Danh Conan Tập 99", Author = "Gosho Aoyama", Price = 22, ImageUrl = "conan.jpg", CategoryId = 1 },
                new Book { BookId = 3, Title = "Naruto Tập 1", Author = "Masashi Kishimoto", Price = 20, ImageUrl = "naruto.jpg", CategoryId = 1 },
                new Book { BookId = 4, Title = "Mắt Biếc", Author = "Nguyễn Nhật Ánh", Price = 85, ImageUrl = "matbiec.jpg", CategoryId = 2 },
                new Book { BookId = 5, Title = "Tôi Thấy Hoa Vàng Trên Cỏ Xanh", Author = "Nguyễn Nhật Ánh", Price = 90, ImageUrl = "hoavang.jpg", CategoryId = 2 }
            );
        }
    }
}