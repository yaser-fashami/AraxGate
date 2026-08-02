using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AraxGate.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class editOilTankConfigs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateEntrances_OilTankTypes_OilTankTypeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropIndex(
                name: "IX_GateEntrances_OilTankTypeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropColumn(
                name: "OilTankTypeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.AlterColumn<int>(
                name: "OilTankTypeId",
                schema: "Operation",
                table: "GateEntrances",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_OilTankTypeId",
                schema: "Operation",
                table: "GateEntrances",
                column: "OilTankTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateEntrances_OilTankTypes_OilTankTypeId",
                schema: "Operation",
                table: "GateEntrances",
                column: "OilTankTypeId",
                principalTable: "OilTankTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateEntrances_OilTankTypes_OilTankTypeId",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropIndex(
                name: "IX_GateEntrances_OilTankTypeId",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.AlterColumn<short>(
                name: "OilTankTypeId",
                schema: "Operation",
                table: "GateEntrances",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OilTankTypeId1",
                schema: "Operation",
                table: "GateEntrances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_OilTankTypeId1",
                schema: "Operation",
                table: "GateEntrances",
                column: "OilTankTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GateEntrances_OilTankTypes_OilTankTypeId1",
                schema: "Operation",
                table: "GateEntrances",
                column: "OilTankTypeId1",
                principalTable: "OilTankTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
