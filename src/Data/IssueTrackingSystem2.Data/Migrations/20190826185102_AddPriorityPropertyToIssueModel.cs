using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTrackingSystem2.Data.Migrations
{
    public partial class AddPriorityPropertyToIssueModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Issues",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Issues");
        }
    }
}
