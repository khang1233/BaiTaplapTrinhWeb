using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab5.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabaseAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AverageScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentID", "AverageScore", "ClassName", "Name" },
                values: new object[,]
                {
                    { 1, 8.5, "CC01", "Nguyen Phi Phuc" },
                    { 2, 7.9000000000000004, "CC01", "Le Van Khang" },
                    { 3, 9.0, "CC02", "Tran Thi Hoa" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
