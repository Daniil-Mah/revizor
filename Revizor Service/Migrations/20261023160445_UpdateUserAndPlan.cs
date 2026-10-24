using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revizor_Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAndPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Facilities_FacilityId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Plans_PlanId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Records_FacilityId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_PlanId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Records",
                newName: "PlanAssigmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Records_UserId",
                table: "Records",
                newName: "IX_Records_PlanAssigmentId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PlanAssigments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "idx_fullname",
                table: "Users",
                columns: new[] { "FirstName", "SecondName", "LastName" });

            migrationBuilder.AddForeignKey(
                name: "FK_Records_PlanAssigments_PlanAssigmentId",
                table: "Records",
                column: "PlanAssigmentId",
                principalTable: "PlanAssigments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_PlanAssigments_PlanAssigmentId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "idx_fullname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PlanAssigments");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PlanAssigmentId",
                table: "Records",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Records_PlanAssigmentId",
                table: "Records",
                newName: "IX_Records_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Records",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "FacilityId",
                table: "Records",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Records",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartAt",
                table: "Records",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_FacilityId",
                table: "Records",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_PlanId",
                table: "Records",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Facilities_FacilityId",
                table: "Records",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Plans_PlanId",
                table: "Records",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
