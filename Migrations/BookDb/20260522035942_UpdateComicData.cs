using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab5.Migrations.BookDb
{
    /// <inheritdoc />
    public partial class UpdateComicData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Author", "ImageUrl", "Price", "Title" },
                values: new object[] { "Eiichiro Oda", "onepiece.jpg", 25.0, "One Piece Tập 100" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Author", "ImageUrl", "Price", "Title" },
                values: new object[] { "Gosho Aoyama", "conan.jpg", 22.0, "Thám Tử Lừng Danh Conan Tập 99" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                columns: new[] { "Author", "CategoryId", "ImageUrl", "Price", "Title" },
                values: new object[] { "Masashi Kishimoto", 1, "naruto.jpg", 20.0, "Naruto Tập 1" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                columns: new[] { "Author", "ImageUrl", "Price", "Title" },
                values: new object[] { "Nguyễn Nhật Ánh", "matbiec.jpg", 85.0, "Mắt Biếc" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CategoryId", "ImageUrl", "Price", "Title" },
                values: new object[] { 5, "Nguyễn Nhật Ánh", 2, "hoavang.jpg", 90.0, "Tôi Thấy Hoa Vàng Trên Cỏ Xanh" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryName",
                value: "Truyện Tranh");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryName",
                value: "Tiểu Thuyết");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryName",
                value: "Khoa Học");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Author", "ImageUrl", "Price", "Title" },
                values: new object[] { "TS. Lê Xuân Việt", "laptrinhc.jpg", 150.0, "Lập trình C" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Author", "ImageUrl", "Price", "Title" },
                values: new object[] { "Cay Horstmann", "corejava.jpg", 120.0, "Core Java: Fundamentals, Volume 1" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                columns: new[] { "Author", "CategoryId", "ImageUrl", "Price", "Title" },
                values: new object[] { "Nguyễn Nhật Ánh", 2, "tuoitho.jpg", 80.0, "Cho tôi xin một vé đi tuổi thơ" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                columns: new[] { "Author", "ImageUrl", "Price", "Title" },
                values: new object[] { "Hải Dớ", "cuocsong.jpg", 61.600000000000001, "Cuộc Sống Rất Giống Cuộc Đời" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryName",
                value: "Lập trình");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryName",
                value: "Cuộc sống");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryName",
                value: "Sức khỏe");
        }
    }
}
