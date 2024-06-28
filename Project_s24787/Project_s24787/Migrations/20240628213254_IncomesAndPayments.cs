using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_s24787.Migrations
{
    /// <inheritdoc />
    public partial class IncomesAndPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentIdPayment",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdContract = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.IdPayment);
                    table.ForeignKey(
                        name: "FK_Payments_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PaymentIdPayment",
                table: "Contracts",
                column: "PaymentIdPayment");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdClient",
                table: "Payments",
                column: "IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Payments_PaymentIdPayment",
                table: "Contracts",
                column: "PaymentIdPayment",
                principalTable: "Payments",
                principalColumn: "IdPayment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Payments_PaymentIdPayment",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_PaymentIdPayment",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "PaymentIdPayment",
                table: "Contracts");
        }
    }
}
