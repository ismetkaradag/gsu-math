using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class lasldslsdalasdldsaldsal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "controller",
                table: "Bildirim",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "otherid",
                table: "Bildirim",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "controller",
                table: "Bildirim");

            migrationBuilder.DropColumn(
                name: "otherid",
                table: "Bildirim");
        }
    }
}
