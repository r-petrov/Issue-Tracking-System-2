using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTrackingSystem2.Data.Migrations
{
    public partial class RemoveIssuePriorityConnenction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Priorities_PriorityId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_PriorityId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Issues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Issues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_PriorityId",
                table: "Issues",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Priorities_PriorityId",
                table: "Issues",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
