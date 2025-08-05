using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class FirstInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigTablesDB",
                columns: table => new
                {
                    MaxUserId = table.Column<long>(type: "bigint", nullable: false),
                    MaxQuizId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UsersDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGameId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<long>(type: "bigint", nullable: false),
                    UserOptions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersDataDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: true),
                    Points = table.Column<long>(type: "bigint", nullable: true),
                    NumberCreatedQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberLikedQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberFavoriteQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberFinishedChallenges = table.Column<long>(type: "bigint", nullable: true),
                    NumberSolvedTestQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberSolvedNormalQuizzes = table.Column<long>(type: "bigint", nullable: true),
                    NumberCreatedQuizzesChallengers = table.Column<long>(type: "bigint", nullable: true)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigTablesDB");

            migrationBuilder.DropTable(
                name: "UsersDataDB");

            migrationBuilder.DropTable(
                name: "UsersDB");
        }
    }
}
