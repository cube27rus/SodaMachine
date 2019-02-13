using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SodaMachine.Domain.Migrations
{
    public partial class AddModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinsInMachine_SodaDevice_SodaMachineId",
                table: "CoinsInMachine");

            migrationBuilder.DropTable(
                name: "SodaInMachine");

            migrationBuilder.DropTable(
                name: "SodaDevice");

            migrationBuilder.DropIndex(
                name: "IX_CoinsInMachine_SodaMachineId",
                table: "CoinsInMachine");

            migrationBuilder.DropColumn(
                name: "SodaMachineId",
                table: "CoinsInMachine");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Soda",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Soda");

            migrationBuilder.AddColumn<int>(
                name: "SodaMachineId",
                table: "CoinsInMachine",
                nullable: false,
                defaultValue: 0);

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
                name: "SodaInMachine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_CoinsInMachine_SodaDevice_SodaMachineId",
                table: "CoinsInMachine",
                column: "SodaMachineId",
                principalTable: "SodaDevice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
