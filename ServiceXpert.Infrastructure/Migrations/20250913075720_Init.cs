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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssuePriorities",
                columns: table => new
                {
                    IssuePriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePriorities", x => x.IssuePriorityId)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "IssueStatuses",
                columns: table => new
                {
                    IssueStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatuses", x => x.IssueStatusId)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserProfiles", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_AspNetUserProfiles_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    IssueStatusId = table.Column<int>(type: "int", nullable: false),
                    IssuePriorityId = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.IssueId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Issues_IssuePriorities_IssuePriorityId",
                        column: x => x.IssuePriorityId,
                        principalTable: "IssuePriorities",
                        principalColumn: "IssuePriorityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_IssueStatuses_IssueStatusId",
                        column: x => x.IssueStatusId,
                        principalTable: "IssueStatuses",
                        principalColumn: "IssueStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Comments_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId", "Name", "NormalizedName" },
                values: new object[] { new Guid("2b954289-7678-4bd1-a3a3-171eb48346b2"), null, new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), null, null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreateDate", "CreateUserId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifyDate", "ModifyUserId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e45acefa-74b0-4f28-b81f-fbc02d9778b5"), 0, "CD774732-9643-4427-A745-B72B3EAB125D", new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "noemail@noemail.com", false, false, null, null, null, "NOEMAIL@NOEMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEA8/ykwd1Q9T24ymPfU/+GTDJfzc81J27jOD+oMtfBQPHISby25dFCc9ZTbIvgVOUg==", null, false, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "IssuePriorities",
                columns: new[] { "IssuePriorityId", "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "Outage" },
                    { 2, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "Critical" },
                    { 3, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "High" },
                    { 4, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "Medium" },
                    { 5, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "Low" }
                });

            migrationBuilder.InsertData(
                table: "IssueStatuses",
                columns: new[] { "IssueStatusId", "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, "New" },
                    { 2, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, "For Analysis" },
                    { 3, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, "In Progress" },
                    { 4, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, "Resolved" },
                    { 5, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserProfiles",
                columns: new[] { "Id", "CreateDate", "CreateUserId", "FirstName", "LastName", "ModifyDate", "ModifyUserId" },
                values: new object[] { new Guid("e45acefa-74b0-4f28-b81f-fbc02d9778b5"), new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", "Admin", null, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId" },
                values: new object[] { new Guid("2b954289-7678-4bd1-a3a3-171eb48346b2"), new Guid("e45acefa-74b0-4f28-b81f-fbc02d9778b5"), new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), null, null });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "IssuePriorities");

            migrationBuilder.DropTable(
                name: "IssueStatuses");
        }
    }
}
