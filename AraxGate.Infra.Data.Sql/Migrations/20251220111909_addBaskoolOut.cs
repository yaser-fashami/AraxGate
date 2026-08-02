using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AraxGate.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class addBaskoolOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaskoolOut",
                schema: "Operation",
                table: "GateEntrances",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaskoolOut",
                schema: "Operation",
                table: "GateEntrances");

        }
    }
}
