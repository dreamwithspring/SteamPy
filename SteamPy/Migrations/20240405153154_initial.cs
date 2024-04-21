using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace steamPy.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePriceInfoHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<string>(type: "TEXT", nullable: false),
                    GameName = table.Column<string>(type: "TEXT", nullable: true),
                    GameNameCN = table.Column<string>(type: "TEXT", nullable: true),
                    SteamUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SteamPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    SteamPriceNow = table.Column<decimal>(type: "TEXT", nullable: true),
                    SteamPriceHistory = table.Column<decimal>(type: "TEXT", nullable: true),
                    AllLowPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    PriceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DayLowPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    AvgPrice = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePriceInfoHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamePriceInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<string>(type: "TEXT", nullable: false),
                    GameName = table.Column<string>(type: "TEXT", nullable: true),
                    GameNameCN = table.Column<string>(type: "TEXT", nullable: true),
                    SteamUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SteamPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    SteamPriceNow = table.Column<decimal>(type: "TEXT", nullable: true),
                    SteamPriceHistory = table.Column<decimal>(type: "TEXT", nullable: true),
                    AllLowPrice = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePriceInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Salt = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWatchList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    GameId = table.Column<string>(type: "TEXT", nullable: false),
                    GameName = table.Column<string>(type: "TEXT", nullable: true),
                    GameNameCN = table.Column<string>(type: "TEXT", nullable: true),
                    SteamUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SendMailPrice = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchList", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePriceInfoHistories");

            migrationBuilder.DropTable(
                name: "GamePriceInfos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserWatchList");
        }
    }
}
