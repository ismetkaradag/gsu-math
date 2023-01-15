using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsumath.Migrations
{
    /// <inheritdoc />
    public partial class selam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
