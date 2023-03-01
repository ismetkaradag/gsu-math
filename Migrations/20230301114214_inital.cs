using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bildirim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Yazi = table.Column<string>(type: "TEXT", nullable: true),
                    from = table.Column<string>(type: "TEXT", nullable: true),
                    isread = table.Column<bool>(name: "is_read", type: "INTEGER", nullable: false),
                    to = table.Column<string>(type: "TEXT", nullable: true),
                    controller = table.Column<string>(type: "TEXT", nullable: true),
                    action = table.Column<string>(type: "TEXT", nullable: true),
                    otherid = table.Column<string>(type: "TEXT", nullable: true),
                    Atcreated = table.Column<DateTime>(name: "At_created", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bildirim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    AtCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Metin = table.Column<string>(type: "TEXT", nullable: true),
                    isactive = table.Column<bool>(name: "is_active", type: "INTEGER", nullable: false),
                    slug = table.Column<string>(type: "TEXT", nullable: true),
                    inediting = table.Column<string>(name: "in_editing", type: "TEXT", nullable: true),
                    duzenlememetni = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
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
                name: "Etkinlik",
                columns: table => new
                {
                    EtkinlikId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Baslik = table.Column<string>(type: "TEXT", nullable: true),
                    Metin = table.Column<string>(type: "TEXT", nullable: true),
                    Foto = table.Column<string>(type: "TEXT", nullable: true),
                    atCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    startDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    endDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinlik", x => x.EtkinlikId);
                });

            migrationBuilder.CreateTable(
                name: "ForumBaslik",
                columns: table => new
                {
                    ForumBaslikId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Baslik = table.Column<string>(type: "TEXT", nullable: false),
                    AtCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    slug = table.Column<string>(type: "TEXT", nullable: true),
                    creater = table.Column<string>(type: "TEXT", nullable: true),
                    begenisayisi = table.Column<int>(type: "INTEGER", nullable: false),
                    cevapsayisi = table.Column<int>(type: "INTEGER", nullable: false)
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
                    username = table.Column<string>(type: "TEXT", nullable: true),
                    ForumBaslikId = table.Column<int>(type: "INTEGER", nullable: false),
                    Metin = table.Column<string>(type: "TEXT", nullable: true),
                    BegeniSayisi = table.Column<int>(type: "INTEGER", nullable: false),
                    atcreated = table.Column<DateTime>(name: "at_created", type: "TEXT", nullable: false),
                    faydalibulanlar = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumCevap", x => x.ForumCevapId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    AdSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ConfirmPwd = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AtCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Isadmin = table.Column<bool>(name: "Is_admin", type: "INTEGER", nullable: false),
                    isvalidate = table.Column<bool>(name: "is_validate", type: "INTEGER", nullable: false),
                    ismailvalidated = table.Column<bool>(name: "is_mail_validated", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bildirim");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Duyuru");

            migrationBuilder.DropTable(
                name: "Etkinlik");

            migrationBuilder.DropTable(
                name: "ForumBaslik");

            migrationBuilder.DropTable(
                name: "ForumCevap");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
