using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AraxGate.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddOilTankGateOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommodityTypes",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommodityTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommodityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consignees",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsigneeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConsigneeNameEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ConsigneeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consignees_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consignees_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OilTankTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TankType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TankGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilTankTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckTypes",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GateEntrances",
                schema: "Operation",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GateInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GateOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GateInOperatorById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GateInOperatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GateOutOperatorById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GateOutOperatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TruckNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TruckNoletter = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PlateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GateEntranceNo = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    GateInImageName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GateOutImageName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Baskool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GateInWeight = table.Column<float>(type: "real", nullable: false),
                    GateOutWeight = table.Column<float>(type: "real", nullable: true),
                    CustomPermissionNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TruckTypeId = table.Column<int>(type: "int", nullable: false),
                    ConsigneeId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ConsigneeId1 = table.Column<long>(type: "bigint", nullable: false),
                    CommodityTypeId = table.Column<short>(type: "smallint", nullable: false),
                    CommodityTypeId1 = table.Column<int>(type: "int", nullable: false),
                    OilTankTypeId = table.Column<short>(type: "smallint", nullable: false),
                    OilTankTypeId1 = table.Column<int>(type: "int", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateEntrances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateEntrances_AspNetUsers_GateInOperatorId",
                        column: x => x.GateInOperatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GateEntrances_AspNetUsers_GateOutOperatorId",
                        column: x => x.GateOutOperatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GateEntrances_CommodityTypes_CommodityTypeId1",
                        column: x => x.CommodityTypeId1,
                        principalSchema: "Basic",
                        principalTable: "CommodityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GateEntrances_Consignees_ConsigneeId1",
                        column: x => x.ConsigneeId1,
                        principalSchema: "Basic",
                        principalTable: "Consignees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GateEntrances_OilTankTypes_OilTankTypeId1",
                        column: x => x.OilTankTypeId1,
                        principalTable: "OilTankTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GateEntrances_TruckTypes_TruckTypeId",
                        column: x => x.TruckTypeId,
                        principalSchema: "Basic",
                        principalTable: "TruckTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consignees_CreatedById",
                schema: "Basic",
                table: "Consignees",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Consignees_ModifiedById",
                schema: "Basic",
                table: "Consignees",
                column: "ModifiedById");

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

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_GateInOperatorId",
                schema: "Operation",
                table: "GateEntrances",
                column: "GateInOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_GateOutOperatorId",
                schema: "Operation",
                table: "GateEntrances",
                column: "GateOutOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_OilTankTypeId1",
                schema: "Operation",
                table: "GateEntrances",
                column: "OilTankTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_GateEntrances_TruckTypeId",
                schema: "Operation",
                table: "GateEntrances",
                column: "TruckTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateEntrances",
                schema: "Operation");

            migrationBuilder.DropTable(
                name: "CommodityTypes",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "Consignees",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "OilTankTypes");

            migrationBuilder.DropTable(
                name: "TruckTypes",
                schema: "Basic");

        }
    }
}
