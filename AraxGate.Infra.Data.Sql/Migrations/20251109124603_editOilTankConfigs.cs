using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AraxGate.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class editOilTankConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateEntrances_CommodityTypes_CommodityTypeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropForeignKey(
                name: "FK_GateEntrances_Consignees_ConsigneeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropIndex(
                name: "IX_GateEntrances_CommodityTypeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropIndex(
                name: "IX_GateEntrances_ConsigneeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropColumn(
                name: "CommodityTypeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropColumn(
                name: "ConsigneeId1",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.AlterColumn<long>(
                name: "ConsigneeId",
                schema: "Operation",
                table: "GateEntrances",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<int>(
                name: "CommodityTypeId",
                schema: "Operation",
                table: "GateEntrances",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_CommodityTypeId",
                schema: "Operation",
                table: "GateEntrances",
                column: "CommodityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_ConsigneeId",
                schema: "Operation",
                table: "GateEntrances",
                column: "ConsigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateEntrances_CommodityTypes_CommodityTypeId",
                schema: "Operation",
                table: "GateEntrances",
                column: "CommodityTypeId",
                principalSchema: "Basic",
                principalTable: "CommodityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GateEntrances_Consignees_ConsigneeId",
                schema: "Operation",
                table: "GateEntrances",
                column: "ConsigneeId",
                principalSchema: "Basic",
                principalTable: "Consignees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateEntrances_CommodityTypes_CommodityTypeId",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropForeignKey(
                name: "FK_GateEntrances_Consignees_ConsigneeId",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropIndex(
                name: "IX_GateEntrances_CommodityTypeId",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.DropIndex(
                name: "IX_GateEntrances_ConsigneeId",
                schema: "Operation",
                table: "GateEntrances");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConsigneeId",
                schema: "Operation",
                table: "GateEntrances",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<short>(
                name: "CommodityTypeId",
                schema: "Operation",
                table: "GateEntrances",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommodityTypeId1",
                schema: "Operation",
                table: "GateEntrances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ConsigneeId1",
                schema: "Operation",
                table: "GateEntrances",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_CommodityTypeId1",
                schema: "Operation",
                table: "GateEntrances",
                column: "CommodityTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_ConsigneeId1",
                schema: "Operation",
                table: "GateEntrances",
                column: "ConsigneeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GateEntrances_CommodityTypes_CommodityTypeId1",
                schema: "Operation",
                table: "GateEntrances",
                column: "CommodityTypeId1",
                principalSchema: "Basic",
                principalTable: "CommodityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GateEntrances_Consignees_ConsigneeId1",
                schema: "Operation",
                table: "GateEntrances",
                column: "ConsigneeId1",
                principalSchema: "Basic",
                principalTable: "Consignees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
