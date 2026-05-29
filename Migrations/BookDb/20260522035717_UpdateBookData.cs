using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab5.Migrations.BookDb
{
    /// <inheritdoc />
    public partial class UpdateBookData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Author", "ImageUrl", "Title" },
                values: new object[] { "TS. Lê Xuân Việt", "laptrinhc.jpg", "Lập trình C" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Author", "ImageUrl", "Title" },
                values: new object[] { "Cay Horstmann", "corejava.jpg", "Core Java: Fundamentals, Volume 1" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CategoryId", "ImageUrl", "Price", "Title" },
                values: new object[] { 4, "Hải Dớ", 2, "cuocsong.jpg", 61.600000000000001, "Cuộc Sống Rất Giống Cuộc Đời" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Author", "ImageUrl", "Title" },
                values: new object[] { "Phúc Nguyễn", "csharp.jpg", "Lập trình C# nâng cao" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Author", "ImageUrl", "Title" },
                values: new object[] { "Lê Khang", "efcore.jpg", "Nhập môn EF Core" });
        }
    }
}
