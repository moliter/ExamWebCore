using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnecter.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoiceAnswer = table.Column<int>(type: "int", nullable: true),
                    TxtAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionChoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoiceA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChoiceB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChoiceC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChoiceD = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionChoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userStudents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTeachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionChoiceId = table.Column<int>(type: "int", nullable: true),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionContext_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionContext_QuestionChoice_QuestionChoiceId",
                        column: x => x.QuestionChoiceId,
                        principalTable: "QuestionChoice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "userStudentClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserStudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userStudentClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userStudentClasses_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userStudentClasses_userStudents_UserStudentId",
                        column: x => x.UserStudentId,
                        principalTable: "userStudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<float>(type: "real", nullable: false),
                    UserTeacherId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exams_userTeachers_UserTeacherId",
                        column: x => x.UserTeacherId,
                        principalTable: "userTeachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userTeacherClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTeacherId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTeacherClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userTeacherClasses_classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userTeacherClasses_userTeachers_UserTeacherId",
                        column: x => x.UserTeacherId,
                        principalTable: "userTeachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionContextId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questions_exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_questions_QuestionContext_QuestionContextId",
                        column: x => x.QuestionContextId,
                        principalTable: "QuestionContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    UserStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_studentExams_exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentExams_userStudents_UserStudentId",
                        column: x => x.UserStudentId,
                        principalTable: "userStudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserStudentId = table.Column<int>(type: "int", nullable: false),
                    ChoiceAnswer = table.Column<int>(type: "int", nullable: true),
                    TxtAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQuestionAnswers_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentQuestionAnswers_userStudents_UserStudentId",
                        column: x => x.UserStudentId,
                        principalTable: "userStudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentExamId = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_studentExams_StudentExamId",
                        column: x => x.StudentExamId,
                        principalTable: "studentExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exams_UserTeacherId",
                table: "exams",
                column: "UserTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionContext_AnswerId",
                table: "QuestionContext",
                column: "AnswerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionContext_QuestionChoiceId",
                table: "QuestionContext",
                column: "QuestionChoiceId",
                unique: true,
                filter: "[QuestionChoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_questions_ExamId",
                table: "questions",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_QuestionContextId",
                table: "questions",
                column: "QuestionContextId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_StudentExamId",
                table: "Score",
                column: "StudentExamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_studentExams_ExamId",
                table: "studentExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_studentExams_UserStudentId",
                table: "studentExams",
                column: "UserStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestionAnswers_QuestionId",
                table: "StudentQuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestionAnswers_UserStudentId",
                table: "StudentQuestionAnswers",
                column: "UserStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_userStudentClasses_ClassId",
                table: "userStudentClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_userStudentClasses_UserStudentId",
                table: "userStudentClasses",
                column: "UserStudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userTeacherClasses_ClassId",
                table: "userTeacherClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_userTeacherClasses_UserTeacherId",
                table: "userTeacherClasses",
                column: "UserTeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "StudentQuestionAnswers");

            migrationBuilder.DropTable(
                name: "userStudentClasses");

            migrationBuilder.DropTable(
                name: "userTeacherClasses");

            migrationBuilder.DropTable(
                name: "studentExams");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "userStudents");

            migrationBuilder.DropTable(
                name: "exams");

            migrationBuilder.DropTable(
                name: "QuestionContext");

            migrationBuilder.DropTable(
                name: "userTeachers");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "QuestionChoice");
        }
    }
}
