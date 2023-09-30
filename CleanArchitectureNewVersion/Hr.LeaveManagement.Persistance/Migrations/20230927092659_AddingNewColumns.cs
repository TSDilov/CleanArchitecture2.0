using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 27, 12, 26, 58, 931, DateTimeKind.Local).AddTicks(662), new DateTime(2023, 9, 27, 12, 26, 58, 931, DateTimeKind.Local).AddTicks(664) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 27, 12, 26, 58, 931, DateTimeKind.Local).AddTicks(670), new DateTime(2023, 9, 27, 12, 26, 58, 931, DateTimeKind.Local).AddTicks(671) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2180), new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2232) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2235), new DateTime(2023, 9, 10, 13, 47, 45, 350, DateTimeKind.Local).AddTicks(2236) });
        }
    }
}
