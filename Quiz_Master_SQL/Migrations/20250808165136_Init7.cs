using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "MultiplyAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SingleChoiceQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiplyAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiplyAnswers_TrueOrFalseQuestions_SingleChoiceQuestionId",
                        column: x => x.SingleChoiceQuestionId,
                        principalTable: "TrueOrFalseQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrueOrFalseQuestions_QuizDBId",
                table: "TrueOrFalseQuestions",
                column: "QuizDBId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiplyAnswers_SingleChoiceQuestionId",
                table: "MultiplyAnswers",
                column: "SingleChoiceQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrueOrFalseQuestions_QuizzesDB_QuizDBId",
                table: "TrueOrFalseQuestions",
                column: "QuizDBId",
                principalTable: "QuizzesDB",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrueOrFalseQuestions_QuizzesDB_QuizDBId",
                table: "TrueOrFalseQuestions");

            migrationBuilder.DropTable(
                name: "MultiplyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_TrueOrFalseQuestions_QuizDBId",
                table: "TrueOrFalseQuestions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TrueOrFalseQuestions");

            migrationBuilder.DropColumn(
                name: "QuizDBId",
                table: "TrueOrFalseQuestions");
        }
    }
}
