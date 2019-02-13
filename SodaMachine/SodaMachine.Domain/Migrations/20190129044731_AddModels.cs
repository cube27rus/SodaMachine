using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SodaMachine.Domain.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false),
                    IsAvalible = table.Column<bool>(nullable: false),
                    CoinType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SodaDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SodaDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinsInMachine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    CoinId = table.Column<int>(nullable: false),
                    SodaMachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinsInMachine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinsInMachine_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinsInMachine_SodaDevice_SodaMachineId",
                        column: x => x.SodaMachineId,
                        principalTable: "SodaDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SodaInMachine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    SodaId = table.Column<int>(nullable: false),
                    SodaMachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SodaInMachine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SodaInMachine_Soda_SodaId",
                        column: x => x.SodaId,
                        principalTable: "Soda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SodaInMachine_SodaDevice_SodaMachineId",
                        column: x => x.SodaMachineId,
                        principalTable: "SodaDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinsInMachine_CoinId",
                table: "CoinsInMachine",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinsInMachine_SodaMachineId",
                table: "CoinsInMachine",
                column: "SodaMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_SodaInMachine_SodaId",
                table: "SodaInMachine",
                column: "SodaId");

            migrationBuilder.CreateIndex(
                name: "IX_SodaInMachine_SodaMachineId",
                table: "SodaInMachine",
                column: "SodaMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinsInMachine");

            migrationBuilder.DropTable(
                name: "SodaInMachine");

            migrationBuilder.DropTable(
                name: "Coin");

            migrationBuilder.DropTable(
                name: "SodaDevice");
        }
    }
}
