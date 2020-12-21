using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ph_UserEnv.Migrations
{
    public partial class testwqqq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    game_name = table.Column<string>(nullable: true),
                    creator_id = table.Column<string>(nullable: true),
                    current_turn_id = table.Column<string>(nullable: true),
                    winner_id = table.Column<string>(nullable: true),
                    moves_left = table.Column<string>(nullable: true),
                    game_state = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    creatorId = table.Column<string>(nullable: true),
                    winnerId = table.Column<string>(nullable: true),
                    current_turnId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_GameSessions_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSessions_AspNetUsers_current_turnId",
                        column: x => x.current_turnId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSessions_AspNetUsers_winnerId",
                        column: x => x.winnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    gameSession_id = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    userId = table.Column<string>(nullable: true),
                    gameSessionid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayers", x => x.id);
                    table.ForeignKey(
                        name: "FK_GamePlayers_GameSessions_gameSessionid",
                        column: x => x.gameSessionid,
                        principalTable: "GameSessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamePlayers_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_gameSessionid",
                table: "GamePlayers",
                column: "gameSessionid");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_userId",
                table: "GamePlayers",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_creatorId",
                table: "GameSessions",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_current_turnId",
                table: "GameSessions",
                column: "current_turnId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_winnerId",
                table: "GameSessions",
                column: "winnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayers");

            migrationBuilder.DropTable(
                name: "GameSessions");
        }
    }
}
