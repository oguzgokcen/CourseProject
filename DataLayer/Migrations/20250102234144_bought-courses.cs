using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class boughtcourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentLog_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BoughtCourses",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    BoughtDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    PaymentLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoughtCourses", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BoughtCourses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoughtCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoughtCourses_PaymentLog_PaymentLogId",
                        column: x => x.PaymentLogId,
                        principalTable: "PaymentLog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoughtCourses_PaymentLogId",
                table: "BoughtCourses",
                column: "PaymentLogId");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtCourses_UserId",
                table: "BoughtCourses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentLog_UserId",
                table: "PaymentLog",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "BoughtCourses");

            //migrationBuilder.DropTable(
            //    name: "PaymentLog");

            //migrationBuilder.CreateTable(
            //    name: "AppUserCourse",
            //    columns: table => new
            //    {
            //        BoughtCoursesId = table.Column<int>(type: "int", nullable: false),
            //        UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AppUserCourse", x => new { x.BoughtCoursesId, x.UsersId });
            //        table.ForeignKey(
            //            name: "FK_AppUserCourse_AspNetUsers_UsersId",
            //            column: x => x.UsersId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AppUserCourse_Courses_BoughtCoursesId",
            //            column: x => x.BoughtCoursesId,
            //            principalTable: "Courses",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AppUserCourse_UsersId",
            //    table: "AppUserCourse",
            //    column: "UsersId");
        }
    }
}
