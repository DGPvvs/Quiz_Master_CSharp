using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MPQCorrectAnswerId",
                table: "MultiplyAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MPQFirstAnswerId",
                table: "MultiplyAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MPQSecondAnswerId",
                table: "MultiplyAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MatchingPairsQuestionDB",
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
                    table.PrimaryKey("PK_MatchingPairsQuestionDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchingPairsQuestionDB_QuizzesDB_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzesDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultiplyAnswers_MPQCorrectAnswerId",
                table: "MultiplyAnswers",
                column: "MPQCorrectAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiplyAnswers_MPQFirstAnswerId",
                table: "MultiplyAnswers",
                column: "MPQFirstAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiplyAnswers_MPQSecondAnswerId",
                table: "MultiplyAnswers",
                column: "MPQSecondAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingPairsQuestionDB_QuizId",
                table: "MatchingPairsQuestionDB",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_MatchingPairsQuestionDB_MPQCorrectAnswerId",
                table: "MultiplyAnswers",
                column: "MPQCorrectAnswerId",
                principalTable: "MatchingPairsQuestionDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_MatchingPairsQuestionDB_MPQFirstAnswerId",
                table: "MultiplyAnswers",
                column: "MPQFirstAnswerId",
                principalTable: "MatchingPairsQuestionDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplyAnswers_MatchingPairsQuestionDB_MPQSecondAnswerId",
                table: "MultiplyAnswers",
                column: "MPQSecondAnswerId",
                principalTable: "MatchingPairsQuestionDB",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_MatchingPairsQuestionDB_MPQCorrectAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_MatchingPairsQuestionDB_MPQFirstAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiplyAnswers_MatchingPairsQuestionDB_MPQSecondAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropTable(
                name: "MatchingPairsQuestionDB");

            migrationBuilder.DropIndex(
                name: "IX_MultiplyAnswers_MPQCorrectAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MultiplyAnswers_MPQFirstAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MultiplyAnswers_MPQSecondAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropColumn(
                name: "MPQCorrectAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropColumn(
                name: "MPQFirstAnswerId",
                table: "MultiplyAnswers");

            migrationBuilder.DropColumn(
                name: "MPQSecondAnswerId",
                table: "MultiplyAnswers");
        }
    }
}
