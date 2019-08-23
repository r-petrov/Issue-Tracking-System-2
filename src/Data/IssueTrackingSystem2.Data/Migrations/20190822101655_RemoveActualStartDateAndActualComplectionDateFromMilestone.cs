using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTrackingSystem2.Data.Migrations
{
    public partial class RemoveActualStartDateAndActualComplectionDateFromMilestone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCompletionDate",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                table: "Milestones");

            migrationBuilder.RenameColumn(
                name: "ScheduledStartDate",
                table: "Milestones",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ScheduledCompletionDate",
                table: "Milestones",
                newName: "CompletionDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Milestones",
                newName: "ScheduledStartDate");

            migrationBuilder.RenameColumn(
                name: "CompletionDate",
                table: "Milestones",
                newName: "ScheduledCompletionDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualCompletionDate",
                table: "Milestones",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartDate",
                table: "Milestones",
                nullable: true);
        }
    }
}
