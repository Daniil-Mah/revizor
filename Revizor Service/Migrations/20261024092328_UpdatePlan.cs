using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revizor_Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "PeriodStart",
                table: "Plans",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PeriodEnd",
                table: "Plans",
                type: "date",
                nullable: true,
                defaultValueSql: "(CURRENT_DATE + INTERVAL '1 month')::date",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "(now()::date + interval '1 months')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStart",
                table: "Plans",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodEnd",
                table: "Plans",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "(now()::date + interval '1 months')",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true,
                oldDefaultValueSql: "(CURRENT_DATE + INTERVAL '1 month')::date");
        }
    }
}
