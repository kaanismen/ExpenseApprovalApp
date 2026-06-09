using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseApprovalApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRevisionRequestedAndManagerComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerComment",
                table: "ExpenseRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerComment",
                table: "ExpenseRequests");
        }
    }
}
