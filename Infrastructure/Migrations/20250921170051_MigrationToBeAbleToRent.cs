using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToBeAbleToRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDetail_Rentals_RentalId",
                table: "RentalDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Tools_ToolId",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalDetail",
                table: "RentalDetail");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "RentalDetail");

            migrationBuilder.RenameTable(
                name: "RentalDetail",
                newName: "RentalDetails");

            migrationBuilder.RenameIndex(
                name: "IX_RentalDetail_RentalId",
                table: "RentalDetails",
                newName: "IX_RentalDetails_RentalId");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "Rentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReturnDate",
                table: "Rentals",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "RentalDate",
                table: "Rentals",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalDetails",
                table: "RentalDetails",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "ToolId",
                keyValue: 4,
                column: "QuantityInStock",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "ToolId",
                keyValue: 14,
                column: "QuantityInStock",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "ToolId",
                keyValue: 19,
                column: "QuantityInStock",
                value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetails_ToolId",
                table: "RentalDetails",
                column: "ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDetails_Rentals_RentalId",
                table: "RentalDetails",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "RentalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDetails_Tools_ToolId",
                table: "RentalDetails",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "ToolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Tools_ToolId",
                table: "Rentals",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "ToolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalDetails_Rentals_RentalId",
                table: "RentalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalDetails_Tools_ToolId",
                table: "RentalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Tools_ToolId",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalDetails",
                table: "RentalDetails");

            migrationBuilder.DropIndex(
                name: "IX_RentalDetails_ToolId",
                table: "RentalDetails");

            migrationBuilder.RenameTable(
                name: "RentalDetails",
                newName: "RentalDetail");

            migrationBuilder.RenameIndex(
                name: "IX_RentalDetails_RentalId",
                table: "RentalDetail",
                newName: "IX_RentalDetail_RentalId");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "Rentals",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalDate",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "RentalDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalDetail",
                table: "RentalDetail",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "ToolId",
                keyValue: 4,
                column: "QuantityInStock",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "ToolId",
                keyValue: 14,
                column: "QuantityInStock",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "ToolId",
                keyValue: 19,
                column: "QuantityInStock",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalDetail_Rentals_RentalId",
                table: "RentalDetail",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "RentalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Tools_ToolId",
                table: "Rentals",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "ToolId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
