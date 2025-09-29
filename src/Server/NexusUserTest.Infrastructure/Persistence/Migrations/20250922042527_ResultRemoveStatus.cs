using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusUserTest.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ResultRemoveStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Results");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Results",
                type: "integer",
                nullable: true);
        }
    }
}
