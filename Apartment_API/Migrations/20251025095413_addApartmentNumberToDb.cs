using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apartment_API.Migrations
{
    /// <inheritdoc />
    public partial class addApartmentNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApartmentNumbers",
                columns: table => new
                {
                    ApartmentNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentNumbers", x => x.ApartmentNo);
                });

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 11, 54, 13, 478, DateTimeKind.Local).AddTicks(1366));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 11, 54, 13, 478, DateTimeKind.Local).AddTicks(1416));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 11, 54, 13, 478, DateTimeKind.Local).AddTicks(1418));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 11, 54, 13, 478, DateTimeKind.Local).AddTicks(1420));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 11, 54, 13, 478, DateTimeKind.Local).AddTicks(1422));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentNumbers");

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7453));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7456));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7458));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7461));
        }
    }
}
