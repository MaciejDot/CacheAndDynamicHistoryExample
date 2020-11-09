using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CachePipelineExample.Core.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicEntityImpls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Field = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicEntityImpls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History_BasicEntityImpl",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Id = table.Column<Guid>(nullable: false),
                    Field = table.Column<string>(nullable: true),
                    HistoryInsertDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    HistoryTransactionId = table.Column<Guid>(nullable: false),
                    HistoryChangerId = table.Column<Guid>(nullable: false),
                    HistoryStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_BasicEntityImpl", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "History_Course",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HistoryInsertDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    HistoryTransactionId = table.Column<Guid>(nullable: false),
                    HistoryChangerId = table.Column<Guid>(nullable: false),
                    HistoryStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_Course", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "History_Professor",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    HistoryInsertDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    HistoryTransactionId = table.Column<Guid>(nullable: false),
                    HistoryChangerId = table.Column<Guid>(nullable: false),
                    HistoryStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_Professor", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "History_Student",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    HistoryInsertDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    HistoryTransactionId = table.Column<Guid>(nullable: false),
                    HistoryChangerId = table.Column<Guid>(nullable: false),
                    HistoryStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_Student", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseJoinProfessors",
                columns: table => new
                {
                    ProfessorId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    History_CourseHistoryId = table.Column<Guid>(nullable: true),
                    History_ProfessorHistoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseJoinProfessors", x => new { x.CourseId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_CourseJoinProfessors_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseJoinProfessors_History_Course_History_CourseHistoryId",
                        column: x => x.History_CourseHistoryId,
                        principalTable: "History_Course",
                        principalColumn: "HistoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseJoinProfessors_History_Professor_History_ProfessorHistoryId",
                        column: x => x.History_ProfessorHistoryId,
                        principalTable: "History_Professor",
                        principalColumn: "HistoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseJoinProfessors_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "History_CourseJoinProfessor",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    ProfessorId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    HistoryInsertDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    HistoryTransactionId = table.Column<Guid>(nullable: false),
                    HistoryChangerId = table.Column<Guid>(nullable: false),
                    HistoryStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_CourseJoinProfessor", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_History_CourseJoinProfessor_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_CourseJoinProfessor_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseJoinStudents",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    History_CourseHistoryId = table.Column<Guid>(nullable: true),
                    History_StudentHistoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseJoinStudents", x => new { x.CourseId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_CourseJoinStudents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseJoinStudents_History_Course_History_CourseHistoryId",
                        column: x => x.History_CourseHistoryId,
                        principalTable: "History_Course",
                        principalColumn: "HistoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseJoinStudents_History_Student_History_StudentHistoryId",
                        column: x => x.History_StudentHistoryId,
                        principalTable: "History_Student",
                        principalColumn: "HistoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseJoinStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "History_CourseJoinStudent",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    HistoryInsertDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    HistoryTransactionId = table.Column<Guid>(nullable: false),
                    HistoryChangerId = table.Column<Guid>(nullable: false),
                    HistoryStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_CourseJoinStudent", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_History_CourseJoinStudent_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_CourseJoinStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseJoinProfessors_History_CourseHistoryId",
                table: "CourseJoinProfessors",
                column: "History_CourseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseJoinProfessors_History_ProfessorHistoryId",
                table: "CourseJoinProfessors",
                column: "History_ProfessorHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseJoinProfessors_ProfessorId",
                table: "CourseJoinProfessors",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseJoinStudents_History_CourseHistoryId",
                table: "CourseJoinStudents",
                column: "History_CourseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseJoinStudents_History_StudentHistoryId",
                table: "CourseJoinStudents",
                column: "History_StudentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseJoinStudents_StudentId",
                table: "CourseJoinStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_History_CourseJoinProfessor_CourseId",
                table: "History_CourseJoinProfessor",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_History_CourseJoinProfessor_ProfessorId",
                table: "History_CourseJoinProfessor",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_History_CourseJoinStudent_CourseId",
                table: "History_CourseJoinStudent",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_History_CourseJoinStudent_StudentId",
                table: "History_CourseJoinStudent",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicEntityImpls");

            migrationBuilder.DropTable(
                name: "CourseJoinProfessors");

            migrationBuilder.DropTable(
                name: "CourseJoinStudents");

            migrationBuilder.DropTable(
                name: "History_BasicEntityImpl");

            migrationBuilder.DropTable(
                name: "History_CourseJoinProfessor");

            migrationBuilder.DropTable(
                name: "History_CourseJoinStudent");

            migrationBuilder.DropTable(
                name: "History_Professor");

            migrationBuilder.DropTable(
                name: "History_Course");

            migrationBuilder.DropTable(
                name: "History_Student");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
