using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hr.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeekingDefaultLeaveTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DefaultDays", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator", new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2180), 10, "Administrator", new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2232), "Vacation" },
                    { 2, "Administrator", new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2235), 5, "Administrator", new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2236), "Sick" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
