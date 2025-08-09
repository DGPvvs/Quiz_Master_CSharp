using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_TrueOrFalseQuestions_SingleChoiceQuestionId",
                table: "MultiplyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_TrueOrFalseQuestions_QuizzesDB_QuizDBId",
                table: "TrueOrFalseQuestions");

            migrationBuilder.DropIndex(
                name: "IX_TrueOrFalseQuestions_QuizDBId",
                table: "TrueOrFalseQuestions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TrueOrFalseQuestions");

            migrationBuilder.DropColumn(
                name: "QuizDBId",
                table: "TrueOrFalseQuestions");

            migrationBuilder.CreateTable(
                name: "SingleChoiceQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Point = table.Column<long>(type: "bigint", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleChoiceQuestion_QuizzesDB_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzesDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceQuestion_QuizId",
                table: "SingleChoiceQuestion",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_SingleChoiceQuestion_SingleChoiceQuestionId",
                table: "MultiplyAnswers",
                column: "SingleChoiceQuestionId",
                principalTable: "SingleChoiceQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_SingleChoiceQuestion_SingleChoiceQuestionId",
                table: "MultiplyAnswers");

            migrationBuilder.DropTable(
                name: "SingleChoiceQuestion");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TrueOrFalseQuestions",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "QuizDBId",
                table: "TrueOrFalseQuestions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrueOrFalseQuestions_QuizDBId",
                table: "TrueOrFalseQuestions",
                column: "QuizDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_TrueOrFalseQuestions_SingleChoiceQuestionId",
                table: "MultiplyAnswers",
                column: "SingleChoiceQuestionId",
                principalTable: "TrueOrFalseQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrueOrFalseQuestions_QuizzesDB_QuizDBId",
                table: "TrueOrFalseQuestions",
                column: "QuizDBId",
                principalTable: "QuizzesDB",
                principalColumn: "Id");
        }
    }
}
