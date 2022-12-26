using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class deleteYetki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Yetki");

            migrationBuilder.DropColumn(
                name: "Yetki",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "Is_admin",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_admin",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Yetki",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Yetki",
                columns: table => new
                {
                    YetkiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: true),
                    YetkiHarf = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetki", x => x.YetkiId);
                });
        }
    }
}
