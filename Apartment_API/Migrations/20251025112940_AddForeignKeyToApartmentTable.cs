using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apartment_API.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToApartmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentID",
                table: "ApartmentNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 13, 29, 40, 594, DateTimeKind.Local).AddTicks(3375));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 13, 29, 40, 594, DateTimeKind.Local).AddTicks(3424));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 13, 29, 40, 594, DateTimeKind.Local).AddTicks(3426));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 13, 29, 40, 594, DateTimeKind.Local).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 13, 29, 40, 594, DateTimeKind.Local).AddTicks(3430));

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentNumbers_ApartmentID",
                table: "ApartmentNumbers",
                column: "ApartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentNumbers_Apartments_ApartmentID",
                table: "ApartmentNumbers",
                column: "ApartmentID",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentNumbers_Apartments_ApartmentID",
                table: "ApartmentNumbers");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentNumbers_ApartmentID",
                table: "ApartmentNumbers");

            migrationBuilder.DropColumn(
                name: "ApartmentID",
                table: "ApartmentNumbers");

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
    }
}
