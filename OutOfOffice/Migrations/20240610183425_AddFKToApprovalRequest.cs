using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Migrations
{
    /// <inheritdoc />
    public partial class AddFKToApprovalRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "LeaveRequests",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaveRequests",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests",
                column: "LeaveRequestId",
                principalTable: "LeaveRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_LeaveRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_LeaveRequestId",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "LeaveRequests",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "LeaveRequests",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
