using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AraxGate.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class editConsignee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Basic",
                table: "Consignees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsigneeType",
                schema: "Basic",
                table: "Consignees",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ConsigneeNameEng",
                schema: "Basic",
                table: "Consignees",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "EconomicCode",
                schema: "Basic",
                table: "Consignees",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                schema: "Basic",
                table: "Consignees",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EconomicCode",
                schema: "Basic",
                table: "Consignees");

            migrationBuilder.DropColumn(
                name: "NationalCode",
                schema: "Basic",
                table: "Consignees");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Basic",
                table: "Consignees",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsigneeType",
                schema: "Basic",
                table: "Consignees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ConsigneeNameEng",
                schema: "Basic",
                table: "Consignees",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

        }
    }
}
