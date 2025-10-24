using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Apartment_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedApartmentTableWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7393), "Spacious apartment with modern amenities and a breathtaking city view.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", "Apartment Royale", 4, 250.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7453), "Cozy apartment perfect for a relaxing getaway with family and friends.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg", "Apartment Serenity", 4, 300.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7456), "Luxury apartment featuring a private pool and elegant interior design.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg", "Apartment Luxe", 4, 400.0, 750, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "", new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7458), "Modern apartment with high-end appliances and stunning balcony views.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg", "Apartment Diamond", 4, 550.0, 900, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "", new DateTime(2025, 10, 24, 20, 55, 22, 930, DateTimeKind.Local).AddTicks(7461), "Elegant apartment located near the beach, ideal for a peaceful vacation.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg", "Apartment Elegance", 4, 600.0, 1100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Apartments",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
