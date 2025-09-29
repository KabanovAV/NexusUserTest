using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusUserTest.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ResultAddQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Results_QuestionId",
                table: "Results",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Questions_QuestionId",
                table: "Results",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Questions_QuestionId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_QuestionId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Results");
        }
    }
}
