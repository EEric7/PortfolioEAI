using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEAI.Data.Migrations
{
    /// <inheritdoc />
    public partial class LowFixTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AdminUsers_SkillId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "Skills",
                newName: "IdAdminUser");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_SkillId",
                table: "Skills",
                newName: "IX_Skills_IdAdminUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AdminUsers_IdAdminUser",
                table: "Skills",
                column: "IdAdminUser",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AdminUsers_IdAdminUser",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "IdAdminUser",
                table: "Skills",
                newName: "SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_IdAdminUser",
                table: "Skills",
                newName: "IX_Skills_SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AdminUsers_SkillId",
                table: "Skills",
                column: "SkillId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
