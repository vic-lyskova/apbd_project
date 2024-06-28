using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_s24787.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    IdDiscount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Offer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    ActiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.IdDiscount);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareSystems",
                columns: table => new
                {
                    IdSoftware = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CurrentVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCategory = table.Column<int>(type: "int", nullable: false),
                    LicencePrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareSystems", x => x.IdSoftware);
                    table.ForeignKey(
                        name: "FK_SoftwareSystems_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Firms",
                columns: table => new
                {
                    KRSNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    FirmName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firms", x => x.KRSNumber);
                    table.ForeignKey(
                        name: "FK_Firms_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    PESEL = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.PESEL);
                    table.ForeignKey(
                        name: "FK_Individuals_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Update = table.Column<int>(type: "int", nullable: false),
                    AdditionalUpdates = table.Column<int>(type: "int", nullable: true),
                    IdSoftware = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.IdContract);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_SoftwareSystems_IdSoftware",
                        column: x => x.IdSoftware,
                        principalTable: "SoftwareSystems",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractDiscount",
                columns: table => new
                {
                    ContractsIdContract = table.Column<int>(type: "int", nullable: false),
                    DiscountsIdDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDiscount", x => new { x.ContractsIdContract, x.DiscountsIdDiscount });
                    table.ForeignKey(
                        name: "FK_ContractDiscount_Contracts_ContractsIdContract",
                        column: x => x.ContractsIdContract,
                        principalTable: "Contracts",
                        principalColumn: "IdContract",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractDiscount_Discounts_DiscountsIdDiscount",
                        column: x => x.DiscountsIdDiscount,
                        principalTable: "Discounts",
                        principalColumn: "IdDiscount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "IdCategory", "CategoryName" },
                values: new object[,]
                {
                    { 1, "finanse" },
                    { 2, "edukacja" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "IdClient", "Address", "Email", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Random Street, 1", "firm1.email@email.email", "48123456789" },
                    { 2, "Random Street, 2", "ind1.email@email.email", "987654321" },
                    { 3, "Random Street, 3", "firm2.email@email.email", "48783490128" },
                    { 4, "Random Street, 4", "ind2.email@email.email", "154807654" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "IdDiscount", "ActiveFrom", "ActiveTo", "DiscountName", "Offer", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Welcome back", "Zniżka dla powracających klientów", "5%" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Friday Discount", "Zniżka na subskrypcję", "10%" }
                });

            migrationBuilder.InsertData(
                table: "Firms",
                columns: new[] { "KRSNumber", "FirmName", "IdClient" },
                values: new object[,]
                {
                    { "123456789", "SomeFirmName", 1 },
                    { "67854290654327", "CoolFirmName", 3 }
                });

            migrationBuilder.InsertData(
                table: "Individuals",
                columns: new[] { "PESEL", "IdClient", "Name", "Surname" },
                values: new object[,]
                {
                    { "13579087635", 2, "John", "Doe" },
                    { "75648947261", 4, "Jenny", "Davis" }
                });

            migrationBuilder.InsertData(
                table: "SoftwareSystems",
                columns: new[] { "IdSoftware", "CurrentVersion", "Description", "IdCategory", "LicencePrice", "SoftwareName" },
                values: new object[,]
                {
                    { 1, "Version 1.9.7 - prerelease", "Some finance software. Very cool, everyone should buy", 1, 20000.0, "FinanceSoftware" },
                    { 2, "7.9.8", "Very comfortable for teachers. Students will be smart", 2, 0.0, "EducationSoftware" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDiscount_DiscountsIdDiscount",
                table: "ContractDiscount",
                column: "DiscountsIdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdClient",
                table: "Contracts",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdSoftware",
                table: "Contracts",
                column: "IdSoftware");

            migrationBuilder.CreateIndex(
                name: "IX_Firms_IdClient",
                table: "Firms",
                column: "IdClient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_IdClient",
                table: "Individuals",
                column: "IdClient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSystems_IdCategory",
                table: "SoftwareSystems",
                column: "IdCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDiscount");

            migrationBuilder.DropTable(
                name: "Firms");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "SoftwareSystems");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
