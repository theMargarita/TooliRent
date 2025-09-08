using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BithDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "ToolCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    ToolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.ToolId);
                    table.ForeignKey(
                        name: "FK_Tools_ToolCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ToolCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "ToolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ToolCategories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Electric and battery powered tools for construction and DIY projects", "Power Tools" },
                    { 2, "Manual tools and basic equipment", "Hand Tools" },
                    { 3, "Outdoor and gardening equipment for lawn and landscape maintenance", "Garden Tools" },
                    { 4, "Heavy duty construction and renovation equipment", "Construction Tools" },
                    { 5, "Professional cleaning tools and pressure washing equipment", "Cleaning Equipment" }
                });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "ToolId", "CategoryId", "Description", "Name", "Price", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, 1, "Professional 18V cordless drill with two batteries and charger", "Cordless Drill 18V", 15.99m, 3 },
                    { 2, 1, "Heavy duty circular saw with carbide blade", "Circular Saw 7.25\"", 22.50m, 2 },
                    { 3, 1, "Compact angle grinder for cutting and grinding", "Angle Grinder 4.5\"", 18.99m, 4 },
                    { 4, 1, "Variable speed jigsaw with orbital action", "Jigsaw Variable Speed", 16.75m, 0 },
                    { 5, 1, "High torque impact driver with quick-change chuck", "Impact Driver", 19.99m, 5 },
                    { 6, 1, "Random orbital sander with dust collection", "Orbital Sander", 24.99m, 2 },
                    { 7, 2, "3-piece hammer set: claw, ball peen, and sledge", "Professional Hammer Set", 8.50m, 6 },
                    { 8, 2, "42-piece metric and imperial socket set with ratchets", "Socket Wrench Set", 12.99m, 3 },
                    { 9, 2, "20-piece precision screwdriver set with magnetic tips", "Precision Screwdriver Set", 6.75m, 8 },
                    { 10, 2, "Large tool box with multiple compartments and wheels", "Professional Tool Box", 14.50m, 2 },
                    { 11, 2, "4-piece adjustable wrench set (6\", 8\", 10\", 12\")", "Adjustable Wrench Set", 9.99m, 4 },
                    { 12, 3, "21-inch self-propelled gas lawn mower with mulching capability", "Self-Propelled Lawn Mower", 35.99m, 2 },
                    { 13, 3, "Powerful gas-powered leaf blower with variable speed", "Gas Leaf Blower", 24.99m, 3 },
                    { 14, 3, "24-inch electric hedge trimmer with rotating handle", "Electric Hedge Trimmer", 21.50m, 0 },
                    { 15, 3, "Professional 16-inch gas chainsaw with safety features", "Chainsaw 16\"", 42.99m, 1 },
                    { 16, 3, "Gas-powered string trimmer with dual line head", "String Trimmer", 28.75m, 3 },
                    { 17, 4, "Professional wet tile saw with diamond blade", "Wet Tile Saw 7\"", 55.99m, 1 },
                    { 18, 4, "3.5 cubic feet portable concrete mixer", "Portable Concrete Mixer", 68.50m, 1 },
                    { 19, 4, "35 lb electric demolition hammer with accessories", "Electric Jackhammer", 89.99m, 0 },
                    { 20, 4, "Professional drum floor sander for hardwood floors", "Floor Sander", 75.25m, 1 },
                    { 21, 4, "Heavy duty reciprocating saw with multiple blades", "Reciprocating Saw", 32.99m, 2 },
                    { 22, 5, "3000 PSI electric pressure washer with multiple nozzles", "Electric Pressure Washer", 45.99m, 2 },
                    { 23, 5, "Hot water extraction carpet cleaning machine", "Professional Carpet Cleaner", 39.99m, 1 },
                    { 24, 5, "16-gallon wet/dry shop vacuum with multiple attachments", "Shop Vacuum 16 Gallon", 18.75m, 4 },
                    { 25, 5, "20-inch floor buffer/polisher with pads", "Floor Buffer", 52.50m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rentals",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ToolId",
                table: "Rentals",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "ToolCategories");
        }
    }
}
