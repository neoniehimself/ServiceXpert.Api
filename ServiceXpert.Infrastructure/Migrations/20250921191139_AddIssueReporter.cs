using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceXpert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueReporter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReporterId",
                table: "Issues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUserProfiles_ReporterId",
                table: "Issues",
                column: "ReporterId",
                principalTable: "AspNetUserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUserProfiles_ReporterId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "Issues");
        }
    }
}
