using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class selam2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ForumCevap_ForumCevapId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ForumCevapId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ForumCevapId",
                table: "User");

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
                    isactive = table.Column<bool>(name: "is_active", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.AddColumn<int>(
                name: "ForumCevapId",
                table: "User",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ForumCevapId",
                table: "User",
                column: "ForumCevapId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ForumCevap_ForumCevapId",
                table: "User",
                column: "ForumCevapId",
                principalTable: "ForumCevap",
                principalColumn: "ForumCevapId");
        }
    }
}
