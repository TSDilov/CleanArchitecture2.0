using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hr.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingEmployeeIdIntoLeaveRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 27, 13, 49, 43, 667, DateTimeKind.Local).AddTicks(5377), new DateTime(2023, 9, 27, 13, 49, 43, 667, DateTimeKind.Local).AddTicks(5379) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 27, 13, 49, 43, 667, DateTimeKind.Local).AddTicks(5385), new DateTime(2023, 9, 27, 13, 49, 43, 667, DateTimeKind.Local).AddTicks(5386) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestingEmployeeId",
                table: "LeaveRequests");

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
    }
}
