using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz_Master_SQL.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizDB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizStatus = table.Column<int>(type: "int", nullable: false),
                    NumOfQuestions = table.Column<long>(type: "bigint", nullable: false),
                    Likes = table.Column<long>(type: "bigint", nullable: false),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizDB_UsersDB_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizDB_UserId",
                table: "QuizDB",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizDB");
        }
    }
}
