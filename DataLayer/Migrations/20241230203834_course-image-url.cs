using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class courseimageurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryKeywords_Keyword",
                table: "CategoryKeywords");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchTerm",
                table: "CategoryKeywords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryKeywords_SearchTerm",
                table: "CategoryKeywords",
                column: "SearchTerm",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryKeywords_SearchTerm",
                table: "CategoryKeywords");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SearchTerm",
                table: "CategoryKeywords");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryKeywords_Keyword",
                table: "CategoryKeywords",
                column: "Keyword",
                unique: true);
        }
    }
}
