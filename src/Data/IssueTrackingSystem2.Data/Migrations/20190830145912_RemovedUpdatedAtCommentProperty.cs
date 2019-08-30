using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTrackingSystem2.Data.Migrations
{
    public partial class RemovedUpdatedAtCommentProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Comments",
                nullable: true);
        }
    }
}
