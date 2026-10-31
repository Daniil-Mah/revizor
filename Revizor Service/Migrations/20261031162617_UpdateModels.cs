using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revizor_Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Facilities_FacilityId",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Plans_PlanId",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Users_PassById",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Users_ResponsiblePersonId",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_PlanAssigments_PlanAssigmentId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Plans_PlanId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "ResponsiblePersonId",
                table: "PlanAssigments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Facilities_FacilityId",
                table: "PlanAssigments",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Plans_PlanId",
                table: "PlanAssigments",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Users_PassById",
                table: "PlanAssigments",
                column: "PassById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Users_ResponsiblePersonId",
                table: "PlanAssigments",
                column: "ResponsiblePersonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_PlanAssigments_PlanAssigmentId",
                table: "Records",
                column: "PlanAssigmentId",
                principalTable: "PlanAssigments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Plans_PlanId",
                table: "Reports",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Facilities_FacilityId",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Plans_PlanId",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Users_PassById",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigments_Users_ResponsiblePersonId",
                table: "PlanAssigments");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_PlanAssigments_PlanAssigmentId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Plans_PlanId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResponsiblePersonId",
                table: "PlanAssigments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Facilities_FacilityId",
                table: "PlanAssigments",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Plans_PlanId",
                table: "PlanAssigments",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Users_PassById",
                table: "PlanAssigments",
                column: "PassById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigments_Users_ResponsiblePersonId",
                table: "PlanAssigments",
                column: "ResponsiblePersonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_PlanAssigments_PlanAssigmentId",
                table: "Records",
                column: "PlanAssigmentId",
                principalTable: "PlanAssigments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Plans_PlanId",
                table: "Reports",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
