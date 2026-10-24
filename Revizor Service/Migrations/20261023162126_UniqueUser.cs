using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revizor_Service.Migrations
{
    /// <inheritdoc />
    public partial class UniqueUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_fullname",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "idx_fullname",
                table: "Users",
                columns: new[] { "FirstName", "SecondName", "LastName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_fullname",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "idx_fullname",
                table: "Users",
                columns: new[] { "FirstName", "SecondName", "LastName" });
        }
    }
}
