using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingEmployeeIdIntoLeaveAllocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 27, 12, 33, 30, 8, DateTimeKind.Local).AddTicks(7476), new DateTime(2023, 9, 27, 12, 33, 30, 8, DateTimeKind.Local).AddTicks(7477) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 27, 12, 33, 30, 8, DateTimeKind.Local).AddTicks(7483), new DateTime(2023, 9, 27, 12, 33, 30, 8, DateTimeKind.Local).AddTicks(7484) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveAllocations");

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
    }
}
