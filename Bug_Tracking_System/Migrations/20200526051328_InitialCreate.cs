using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracking_System.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(nullable: true),
                    CteateTime = table.Column<DateTime>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollment_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(nullable: false),
                    TesterId = table.Column<int>(nullable: false),
                    TestCaseName = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    CaseTesterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCase_User_CaseTesterId",
                        column: x => x.CaseTesterId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestCase_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bug",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(nullable: false),
                    TestCaseId = table.Column<int>(nullable: false),
                    BugStatus = table.Column<int>(nullable: false),
                    Severity = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    TesterId = table.Column<int>(nullable: false),
                    TestManagerId = table.Column<int>(nullable: false),
                    DeveloperId = table.Column<int>(nullable: false),
                    BugName = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bug", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bug_User_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bug_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bug_TestCase_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bug_User_TestManagerId",
                        column: x => x.TestManagerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bug_User_TesterId",
                        column: x => x.TesterId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bug_DeveloperId",
                table: "Bug",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_ProjectId",
                table: "Bug",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_TestCaseId",
                table: "Bug",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_TestManagerId",
                table: "Bug",
                column: "TestManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_TesterId",
                table: "Bug",
                column: "TesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ProjectId",
                table: "Enrollment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_UserId",
                table: "Enrollment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_CaseTesterId",
                table: "TestCase",
                column: "CaseTesterId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_ProjectId",
                table: "TestCase",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bug");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "TestCase");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
