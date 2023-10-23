using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoxCar.ShoppingBasket.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasketChangeEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EngineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChassisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionPackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BasketChangeType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketChangeEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketId);
                });

            migrationBuilder.CreateTable(
                name: "Chassis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chassis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    IgnitionMethod = table.Column<int>(type: "int", nullable: false),
                    Strokes = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionPacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionPacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ChassisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EngineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionPackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Chassis_ChassisId",
                        column: x => x.ChassisId,
                        principalTable: "Chassis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_OptionPacks_OptionPackId",
                        column: x => x.OptionPackId,
                        principalTable: "OptionPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketLines",
                columns: table => new
                {
                    BasketLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EngineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChassisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionPackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketLines", x => x.BasketLineId);
                    table.ForeignKey(
                        name: "FK_BasketLines_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);                   
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketLines_BasketId",
                table: "BasketLines",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketLines_VehicleId",
                table: "BasketLines",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ChassisId",
                table: "Vehicles",
                column: "ChassisId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_EngineId",
                table: "Vehicles",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OptionPackId",
                table: "Vehicles",
                column: "OptionPackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketChangeEvents");

            migrationBuilder.DropTable(
                name: "BasketLines");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Chassis");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "OptionPacks");
        }
    }
}
