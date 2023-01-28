using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class hjhjbjhbjh2asdasdads : Migration
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
                    Atcreated = table.Column<DateTime>(name: "At_created", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bildirim", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bildirim");
        }
    }
}
