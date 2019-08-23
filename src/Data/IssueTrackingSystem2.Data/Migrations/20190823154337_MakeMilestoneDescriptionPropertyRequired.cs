using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTrackingSystem2.Data.Migrations
{
    public partial class MakeMilestoneDescriptionPropertyRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Milestones",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Milestones",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
