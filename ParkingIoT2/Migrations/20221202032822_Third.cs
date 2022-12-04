using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingIoT2.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_QRCodes_QRCodeId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "QRCodes");

            migrationBuilder.DropIndex(
                name: "IX_Customers_QRCodeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "QRCodeId",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "RFIDCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFIDCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RFIDCodes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RFIDCodes_Code",
                table: "RFIDCodes",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDCodes_CustomerId",
                table: "RFIDCodes",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RFIDCodes");

            migrationBuilder.AddColumn<Guid>(
                name: "QRCodeId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QRCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCodes", x => x.Id);
                });

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
    }
}
