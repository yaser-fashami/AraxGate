using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AraxGate.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class addBaskoolAndBaskoolSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaskoolOperations",
                schema: "Operation",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaskoolType = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaskoolOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaskoolSettings",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaskoolMACAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BaskoolType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaskoolSettings", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaskoolOperations",
                schema: "Operation");

            migrationBuilder.DropTable(
                name: "BaskoolSettings",
                schema: "Basic");

        }
    }
}
