using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactDotnetTemplate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AppEvents2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "AppEventLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimesSent",
                table: "AppEventLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "AppEventLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "AppEventLogs");

            migrationBuilder.DropColumn(
                name: "TimesSent",
                table: "AppEventLogs");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "AppEventLogs");
        }
    }
}
