using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			Debugger.Launch();
			migrationBuilder.DeleteData(
                table: "ConfigTablesDB",
                keyColumn: "Id",
                keyValue: new Guid("5ba9cd6e-1a8a-4c08-a9a3-bd195dc05a49"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("0ac7744c-6d09-4287-8dd9-ae942b12f1b0"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("3031f5e6-6845-419a-8ddf-abbc563e8dda"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("374a1961-cff2-44ee-9cc4-333698ad0d81"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("51594435-7d14-4084-9798-d4d2348f3cb9"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("6723fd1c-8128-4b36-808d-1053137ac351"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("6f7b651d-0fba-4321-9f2d-4badb7e2ec81"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("9b781b62-bb61-4c3c-8997-6321d474cade"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("9d5c7622-fd27-494f-85b8-bd8bb2cde748"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("f21cb56b-a1bd-4880-b186-03e781ae7157"));

            migrationBuilder.DeleteData(
                table: "UsersDB",
                keyColumn: "Id",
                keyValue: new Guid("f4111ed6-c886-4d34-81cd-63d5fdfaf016"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConfigTablesDB",
                columns: new[] { "Id", "MaxQuizId", "MaxUserId" },
                values: new object[] { new Guid("5ba9cd6e-1a8a-4c08-a9a3-bd195dc05a49"), 0L, 10L });

            migrationBuilder.InsertData(
                table: "UsersDB",
                columns: new[] { "Id", "FirstName", "LastName", "Level", "NumberCreatedQuizzes", "NumberCreatedQuizzesChallengers", "NumberFavoriteQuizzes", "NumberFinishedChallenges", "NumberLikedQuizzes", "NumberSolvedNormalQuizzes", "NumberSolvedTestQuizzes", "Password", "Points", "UserGameId", "UserName", "UserOptions" },
                values: new object[,]
                {
                    { new Guid("0ac7744c-6d09-4287-8dd9-ae942b12f1b0"), "Admin", "Nine", null, null, null, null, null, null, null, null, 708698351L, null, 9L, "admin9", 1 },
                    { new Guid("3031f5e6-6845-419a-8ddf-abbc563e8dda"), "Admin", "Four", null, null, null, null, null, null, null, null, 708698336L, null, 4L, "admin4", 1 },
                    { new Guid("374a1961-cff2-44ee-9cc4-333698ad0d81"), "Admin", "Ten", null, null, null, null, null, null, null, null, 2021029294L, null, 10L, "admin10", 1 },
                    { new Guid("51594435-7d14-4084-9798-d4d2348f3cb9"), "Admin", "Eight", null, null, null, null, null, null, null, null, 708698348L, null, 8L, "admin8", 1 },
                    { new Guid("6723fd1c-8128-4b36-808d-1053137ac351"), "Admin", "Seven", null, null, null, null, null, null, null, null, 708698349L, null, 7L, "admin7", 1 },
                    { new Guid("6f7b651d-0fba-4321-9f2d-4badb7e2ec81"), "Admin", "One", null, null, null, null, null, null, null, null, 708698343L, null, 1L, "admin1", 1 },
                    { new Guid("9b781b62-bb61-4c3c-8997-6321d474cade"), "Admin", "Five", null, null, null, null, null, null, null, null, 708698339L, null, 5L, "admin5", 1 },
                    { new Guid("9d5c7622-fd27-494f-85b8-bd8bb2cde748"), "Admin", "Six", null, null, null, null, null, null, null, null, 708698338L, null, 6L, "admin6", 1 },
                    { new Guid("f21cb56b-a1bd-4880-b186-03e781ae7157"), "Admin", "Two", null, null, null, null, null, null, null, null, 708698342L, null, 2L, "admin2", 1 },
                    { new Guid("f4111ed6-c886-4d34-81cd-63d5fdfaf016"), "Admin", "Three", null, null, null, null, null, null, null, null, 708698337L, null, 3L, "admin3", 1 }
                });
        }
    }
}
