using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revizor_Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanCurrecty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_only_true",
                table: "Plans");

            migrationBuilder.CreateIndex(
                name: "idx_only_true",
                table: "Plans",
                column: "IsArchived",
                unique: true,
                filter: "\"IsArchived\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_only_true",
                table: "Plans");

            migrationBuilder.CreateIndex(
                name: "idx_only_true",
                table: "Plans",
                column: "IsArchived",
                unique: true,
                filter: "\"IsArchived\" = true");
        }
    }
}
