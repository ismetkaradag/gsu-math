using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class newmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bilgi",
                columns: table => new
                {
                    BilgiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tür = table.Column<string>(type: "TEXT", nullable: true),
                    Başlık = table.Column<string>(type: "TEXT", nullable: true),
                    Metin = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilgi", x => x.BilgiId);
                });

            migrationBuilder.CreateTable(
                name: "Duyuru",
                columns: table => new
                {
                    DuyuruId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Baslik = table.Column<string>(type: "TEXT", nullable: true),
                    Foto = table.Column<string>(type: "TEXT", nullable: true),
                    Metin = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyuru", x => x.DuyuruId);
                });

            migrationBuilder.CreateTable(
                name: "ForumBaslik",
                columns: table => new
                {
                    ForumBaslikId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Baslik = table.Column<string>(type: "TEXT", nullable: true),
                    AtCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumBaslik", x => x.ForumBaslikId);
                });

            migrationBuilder.CreateTable(
                name: "ForumCevap",
                columns: table => new
                {
                    ForumCevapId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ForumBaslikId = table.Column<int>(type: "INTEGER", nullable: false),
                    Metin = table.Column<string>(type: "TEXT", nullable: true),
                    BegeniSayisi = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumCevap", x => x.ForumCevapId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bilgi");

            migrationBuilder.DropTable(
                name: "Duyuru");

            migrationBuilder.DropTable(
                name: "ForumBaslik");

            migrationBuilder.DropTable(
                name: "ForumCevap");
        }
    }
}
