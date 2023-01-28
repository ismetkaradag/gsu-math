using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class lasldslsdalasdldsaldsal2221qe222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duzenleme_talebi_active",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "duzenleme_talebi_returned",
                table: "Blog");

            migrationBuilder.AddColumn<string>(
                name: "in_editing",
                table: "Blog",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "in_editing",
                table: "Blog");

            migrationBuilder.AddColumn<bool>(
                name: "duzenleme_talebi_active",
                table: "Blog",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "duzenleme_talebi_returned",
                table: "Blog",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
