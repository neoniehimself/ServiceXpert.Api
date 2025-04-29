using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceXpert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssuePriority",
                columns: table => new
                {
                    IssuePriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePriority", x => x.IssuePriorityId)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "IssueStatus",
                columns: table => new
                {
                    IssueStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatus", x => x.IssueStatusId)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(4096)", nullable: true),
                    IssueStatusId = table.Column<int>(type: "int", nullable: false),
                    IssuePriorityId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.IssueId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Issue_IssuePriority_IssuePriorityId",
                        column: x => x.IssuePriorityId,
                        principalTable: "IssuePriority",
                        principalColumn: "IssuePriorityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issue_IssueStatus_IssueStatusId",
                        column: x => x.IssueStatusId,
                        principalTable: "IssueStatus",
                        principalColumn: "IssueStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "IssuePriority",
                columns: new[] { "IssuePriorityId", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Outage" },
                    { 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Critical" },
                    { 3, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "High" },
                    { 4, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Medium" },
                    { 5, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Low" }
                });

            migrationBuilder.InsertData(
                table: "IssueStatus",
                columns: new[] { "IssueStatusId", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "New" },
                    { 2, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "For Analysis" },
                    { 3, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "In Progress" },
                    { 4, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Resolved" },
                    { 5, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Closed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "IssuePriority");

            migrationBuilder.DropTable(
                name: "IssueStatus");
        }
    }
}
