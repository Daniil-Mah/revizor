using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revizor_Service.Migrations
{
    /// <inheritdoc />
    public partial class Reports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Users_CreatedById",
                table: "Plan");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigment_Facility_FacilityId",
                table: "PlanAssigment");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigment_Plan_PlanId",
                table: "PlanAssigment");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigment_Users_PassById",
                table: "PlanAssigment");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAssigment_Users_ResponsiblePersonId",
                table: "PlanAssigment");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Facility_FacilityId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Plan_PlanId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Users_UserId",
                table: "Record");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Record",
                table: "Record");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanAssigment",
                table: "PlanAssigment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plan",
                table: "Plan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Facility",
                table: "Facility");

            migrationBuilder.RenameTable(
                name: "Record",
                newName: "Records");

            migrationBuilder.RenameTable(
                name: "PlanAssigment",
                newName: "PlanAssigments");

            migrationBuilder.RenameTable(
                name: "Plan",
                newName: "Plans");

            migrationBuilder.RenameTable(
                name: "Facility",
                newName: "Facilities");

            migrationBuilder.RenameIndex(
                name: "IX_Record_UserId",
                table: "Records",
                newName: "IX_Records_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Record_PlanId",
                table: "Records",
                newName: "IX_Records_PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Record_FacilityId",
                table: "Records",
                newName: "IX_Records_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigment_ResponsiblePersonId",
                table: "PlanAssigments",
                newName: "IX_PlanAssigments_ResponsiblePersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigment_PlanId",
                table: "PlanAssigments",
                newName: "IX_PlanAssigments_PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigment_PassById",
                table: "PlanAssigments",
                newName: "IX_PlanAssigments_PassById");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigment_FacilityId",
                table: "PlanAssigments",
                newName: "IX_PlanAssigments_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Plan_Name",
                table: "Plans",
                newName: "IX_Plans_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Plan_CreatedById",
                table: "Plans",
                newName: "IX_Plans_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Facility_Title_Location",
                table: "Facilities",
                newName: "IX_Facilities_Title_Location");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanAssigments",
                table: "PlanAssigments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plans",
                table: "Plans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Facilities",
                table: "Facilities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

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
                name: "FK_Plans_Users_CreatedById",
                table: "Plans",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Plans_Users_CreatedById",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Facilities_FacilityId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Plans_PlanId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plans",
                table: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanAssigments",
                table: "PlanAssigments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Facilities",
                table: "Facilities");

            migrationBuilder.RenameTable(
                name: "Records",
                newName: "Record");

            migrationBuilder.RenameTable(
                name: "Plans",
                newName: "Plan");

            migrationBuilder.RenameTable(
                name: "PlanAssigments",
                newName: "PlanAssigment");

            migrationBuilder.RenameTable(
                name: "Facilities",
                newName: "Facility");

            migrationBuilder.RenameIndex(
                name: "IX_Records_UserId",
                table: "Record",
                newName: "IX_Record_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Records_PlanId",
                table: "Record",
                newName: "IX_Record_PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Records_FacilityId",
                table: "Record",
                newName: "IX_Record_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_Name",
                table: "Plan",
                newName: "IX_Plan_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_CreatedById",
                table: "Plan",
                newName: "IX_Plan_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigments_ResponsiblePersonId",
                table: "PlanAssigment",
                newName: "IX_PlanAssigment_ResponsiblePersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigments_PlanId",
                table: "PlanAssigment",
                newName: "IX_PlanAssigment_PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigments_PassById",
                table: "PlanAssigment",
                newName: "IX_PlanAssigment_PassById");

            migrationBuilder.RenameIndex(
                name: "IX_PlanAssigments_FacilityId",
                table: "PlanAssigment",
                newName: "IX_PlanAssigment_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Facilities_Title_Location",
                table: "Facility",
                newName: "IX_Facility_Title_Location");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Record",
                table: "Record",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plan",
                table: "Plan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanAssigment",
                table: "PlanAssigment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Facility",
                table: "Facility",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Users_CreatedById",
                table: "Plan",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigment_Facility_FacilityId",
                table: "PlanAssigment",
                column: "FacilityId",
                principalTable: "Facility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigment_Plan_PlanId",
                table: "PlanAssigment",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigment_Users_PassById",
                table: "PlanAssigment",
                column: "PassById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAssigment_Users_ResponsiblePersonId",
                table: "PlanAssigment",
                column: "ResponsiblePersonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Facility_FacilityId",
                table: "Record",
                column: "FacilityId",
                principalTable: "Facility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Plan_PlanId",
                table: "Record",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Users_UserId",
                table: "Record",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
