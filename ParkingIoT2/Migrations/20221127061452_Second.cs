using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingIoT2.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QRCodes_Customers_CustomerId",
                table: "QRCodes");

            migrationBuilder.DropIndex(
                name: "IX_QRCodes_CustomerId",
                table: "QRCodes");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "QRCodes");

            migrationBuilder.AddColumn<Guid>(
                name: "QRCodeId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_QRCodeId",
                table: "Customers",
                column: "QRCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_QRCodes_QRCodeId",
                table: "Customers",
                column: "QRCodeId",
                principalTable: "QRCodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_QRCodes_QRCodeId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_QRCodeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "QRCodeId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "QRCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QRCodes_CustomerId",
                table: "QRCodes",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_QRCodes_Customers_CustomerId",
                table: "QRCodes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
