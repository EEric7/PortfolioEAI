using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEAI.Data.Migrations
{
    /// <inheritdoc />
    public partial class HotFixTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Experiences_IdExperience",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AdminUsers_AdminUserId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "AdminUserId",
                table: "Skills",
                newName: "SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_AdminUserId",
                table: "Skills",
                newName: "IX_Skills_SkillId");

            migrationBuilder.RenameColumn(
                name: "IdExperience",
                table: "Projects",
                newName: "IdProject");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_IdExperience",
                table: "Projects",
                newName: "IX_Projects_IdProject");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Experiences_IdProject",
                table: "Projects",
                column: "IdProject",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AdminUsers_SkillId",
                table: "Skills",
                column: "SkillId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Experiences_IdProject",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AdminUsers_SkillId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "Skills",
                newName: "AdminUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_SkillId",
                table: "Skills",
                newName: "IX_Skills_AdminUserId");

            migrationBuilder.RenameColumn(
                name: "IdProject",
                table: "Projects",
                newName: "IdExperience");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_IdProject",
                table: "Projects",
                newName: "IX_Projects_IdExperience");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Experiences_IdExperience",
                table: "Projects",
                column: "IdExperience",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AdminUsers_AdminUserId",
                table: "Skills",
                column: "AdminUserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
