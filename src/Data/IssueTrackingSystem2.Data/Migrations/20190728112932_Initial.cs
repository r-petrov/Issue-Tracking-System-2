using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTrackingSystem2.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Project_ProjectId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Project_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Project_ProjectId",
                table: "Priorities");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_LeaderId",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Project_LeaderId",
                table: "Projects",
                newName: "IX_Projects_LeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_IsDeleted",
                table: "Projects",
                newName: "IX_Projects_IsDeleted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualStartDate",
                table: "Milestones",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualCompletionDate",
                table: "Milestones",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectLabels",
                columns: table => new
                {
                    ProjectId = table.Column<string>(nullable: false),
                    LabelId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLabels", x => new { x.ProjectId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_ProjectLabels_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectLabels_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLabels_LabelId",
                table: "ProjectLabels",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Projects_ProjectId",
                table: "Labels",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Projects_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Projects_ProjectId",
                table: "Priorities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_LeaderId",
                table: "Projects",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Projects_ProjectId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Projects_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Projects_ProjectId",
                table: "Priorities");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_LeaderId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_LeaderId",
                table: "Project",
                newName: "IX_Project_LeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_IsDeleted",
                table: "Project",
                newName: "IX_Project_IsDeleted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualStartDate",
                table: "Milestones",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualCompletionDate",
                table: "Milestones",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Project_ProjectId",
                table: "Labels",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Project_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Project_ProjectId",
                table: "Priorities",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_LeaderId",
                table: "Project",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
