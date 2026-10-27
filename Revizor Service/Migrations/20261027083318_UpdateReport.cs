using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revizor_Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reports_PlanId",
                table: "Reports",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Plans_PlanId",
                table: "Reports",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Plans_PlanId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_PlanId",
                table: "Reports");
        }
    }
}
