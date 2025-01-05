using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InstructorDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "InstructorDetails",
                columns: table => new
                {
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentCount = table.Column<int>(type: "int", nullable: false),
                    CourseCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorDetails", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_InstructorDetails_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_InstructorDetails_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "InstructorDetails",
                principalColumn: "InstructorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_InstructorDetails_InstructorId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "InstructorDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
