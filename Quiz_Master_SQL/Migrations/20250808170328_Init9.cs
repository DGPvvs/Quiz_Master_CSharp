using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_SingleChoiceQuestion_SingleChoiceQuestionId",
                table: "MultiplyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceQuestion_QuizzesDB_QuizId",
                table: "SingleChoiceQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleChoiceQuestion",
                table: "SingleChoiceQuestion");

            migrationBuilder.RenameTable(
                name: "SingleChoiceQuestion",
                newName: "SingleChoiceQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChoiceQuestion_QuizId",
                table: "SingleChoiceQuestions",
                newName: "IX_SingleChoiceQuestions_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleChoiceQuestions",
                table: "SingleChoiceQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_SingleChoiceQuestions_SingleChoiceQuestionId",
                table: "MultiplyAnswers",
                column: "SingleChoiceQuestionId",
                principalTable: "SingleChoiceQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceQuestions_QuizzesDB_QuizId",
                table: "SingleChoiceQuestions",
                column: "QuizId",
                principalTable: "QuizzesDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_SingleChoiceQuestions_SingleChoiceQuestionId",
                table: "MultiplyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceQuestions_QuizzesDB_QuizId",
                table: "SingleChoiceQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleChoiceQuestions",
                table: "SingleChoiceQuestions");

            migrationBuilder.RenameTable(
                name: "SingleChoiceQuestions",
                newName: "SingleChoiceQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChoiceQuestions_QuizId",
                table: "SingleChoiceQuestion",
                newName: "IX_SingleChoiceQuestion_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleChoiceQuestion",
                table: "SingleChoiceQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_SingleChoiceQuestion_SingleChoiceQuestionId",
                table: "MultiplyAnswers",
                column: "SingleChoiceQuestionId",
                principalTable: "SingleChoiceQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceQuestion_QuizzesDB_QuizId",
                table: "SingleChoiceQuestion",
                column: "QuizId",
                principalTable: "QuizzesDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
