using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizDB_UsersDB_UserId",
                table: "QuizDB");

            migrationBuilder.DropTable(
                name: "UsersDataDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizDB",
                table: "QuizDB");

            migrationBuilder.RenameTable(
                name: "QuizDB",
                newName: "QuizzesDB");

            migrationBuilder.RenameIndex(
                name: "IX_QuizDB_UserId",
                table: "QuizzesDB",
                newName: "IX_QuizzesDB_UserId");

            migrationBuilder.AddColumn<long>(
                name: "Level",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberCreatedQuizzes",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberCreatedQuizzesChallengers",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberFavoriteQuizzes",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberFinishedChallenges",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberLikedQuizzes",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberSolvedNormalQuizzes",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NumberSolvedTestQuizzes",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Points",
                table: "UsersDB",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizzesDB",
                table: "QuizzesDB",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FavoritedQuizzes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritedQuizzes", x => new { x.QuizId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavoritedQuizzes_QuizzesDB_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzesDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoritedQuizzes_UsersDB_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinishedChallengesDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChallengeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedChallengesDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedChallengesDB_UsersDB_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedQuizzesDB",
                columns: table => new
                {
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedQuizzesDB", x => new { x.QuizId, x.UserId });
                    table.ForeignKey(
                        name: "FK_LikedQuizzesDB_QuizzesDB_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizzesDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikedQuizzesDB_UsersDB_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritedQuizzes_UserId",
                table: "FavoritedQuizzes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedChallengesDB_UserId",
                table: "FinishedChallengesDB",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedQuizzesDB_UserId",
                table: "LikedQuizzesDB",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesDB_UsersDB_UserId",
                table: "QuizzesDB",
                column: "UserId",
                principalTable: "UsersDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesDB_UsersDB_UserId",
                table: "QuizzesDB");

            migrationBuilder.DropTable(
                name: "FavoritedQuizzes");

            migrationBuilder.DropTable(
                name: "FinishedChallengesDB");

            migrationBuilder.DropTable(
                name: "LikedQuizzesDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizzesDB",
                table: "QuizzesDB");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "NumberCreatedQuizzes",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "NumberCreatedQuizzesChallengers",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "NumberFavoriteQuizzes",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "NumberFinishedChallenges",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "NumberLikedQuizzes",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "NumberSolvedNormalQuizzes",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "NumberSolvedTestQuizzes",
                table: "UsersDB");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "UsersDB");

            migrationBuilder.RenameTable(
                name: "QuizzesDB",
                newName: "QuizDB");

            migrationBuilder.RenameIndex(
                name: "IX_QuizzesDB_UserId",
                table: "QuizDB",
                newName: "IX_QuizDB_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizDB",
                table: "QuizDB",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UsersDataDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: true),
                    NumberCreatedQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberCreatedQuizzesChallengers = table.Column<long>(type: "bigint", nullable: true),
                    NumberFavoriteQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberFinishedChallenges = table.Column<long>(type: "bigint", nullable: true),
                    NumberLikedQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberSolvedNormalQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberSolvedTestQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    Points = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDataDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersDataDB_UsersDB_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersDataDB_UserId",
                table: "UsersDataDB",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizDB_UsersDB_UserId",
                table: "QuizDB",
                column: "UserId",
                principalTable: "UsersDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
