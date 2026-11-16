using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogCenterFoundations.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Notes", "PhoneNumber" },
                values: new object[] { 1, "elena.fischer@example.com", "Elena", "Fischer", "Responsible owner. Actively involved in training and socialization. No outstanding payments.", "+4935900000000" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Price", "ServiceName", "ServiceType" },
                values: new object[,]
                {
                    { 1, "10 group lessons for puppies up to 8 months. Fixed schedule, covering basic obedience, socialization, and building a strong relationship with your puppy.", 550m, "Puppy Group Training", "School" },
                    { 2, "Professional nail trimming to keep your dog's paws healthy and comfortable.", 30m, "Nail Trimming", "Salon" },
                    { 3, "Supervised daytime care in a safe and welcoming environment. Available daily from 08:00 to 19:00. Price per day.", 15m, "Daycare", "Hotel" },
                    { 4, "Monthly subscription including unlimited daycare and overnight stays, plus a 5% discount on all other services.", 600m, "VIP Subscription", "Misc" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Birthday", "Breed", "IsVip", "MicrochipId", "Name", "Notes", "OwnerId", "RegistrationDate" },
                values: new object[] { 1, new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "German Shepherd", true, "276098100123456", "Onyx", "Friendly, energetic, and enthusiastic about training. Loves treats. Crazy about fetch.", 1, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "Id", "Brand", "Date", "DogId", "Name", "NextDueDate", "Notes", "RecordType" },
                values: new object[] { 1, "Rabisin", new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rabies Vaccination", new DateTime(2028, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Routine vaccination completed without complications. Recommended supplements for joint health.", "Vaccination" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicalRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
