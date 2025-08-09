using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MultipleChoiceQuestionCorrectAnswerId",
                table: "MultiplyAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MultipleChoiceQuestionId",
                table: "MultiplyAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MultipleChoiceQuestionDB",
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
                    table.PrimaryKey("PK_MultipleChoiceQuestionDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceQuestionDB_QuizzesDB_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzesDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultiplyAnswers_MultipleChoiceQuestionCorrectAnswerId",
                table: "MultiplyAnswers",
                column: "MultipleChoiceQuestionCorrectAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiplyAnswers_MultipleChoiceQuestionId",
                table: "MultiplyAnswers",
                column: "MultipleChoiceQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceQuestionDB_QuizId",
                table: "MultipleChoiceQuestionDB",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_MultipleChoiceQuestionDB_MultipleChoiceQuestionCorrectAnswerId",
                table: "MultiplyAnswers",
                column: "MultipleChoiceQuestionCorrectAnswerId",
                principalTable: "MultipleChoiceQuestionDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_MultipleChoiceQuestionDB_MultipleChoiceQuestionId",
                table: "MultiplyAnswers",
                column: "MultipleChoiceQuestionId",
                principalTable: "MultipleChoiceQuestionDB",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_MultipleChoiceQuestionDB_MultipleChoiceQuestionCorrectAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_MultipleChoiceQuestionDB_MultipleChoiceQuestionId",
                table: "MultiplyAnswers");

            migrationBuilder.DropTable(
                name: "MultipleChoiceQuestionDB");

            migrationBuilder.DropIndex(
                name: "IX_MultiplyAnswers_MultipleChoiceQuestionCorrectAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MultiplyAnswers_MultipleChoiceQuestionId",
                table: "MultiplyAnswers");

            migrationBuilder.DropColumn(
                name: "MultipleChoiceQuestionCorrectAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropColumn(
                name: "MultipleChoiceQuestionId",
                table: "MultiplyAnswers");
        }
    }
}
