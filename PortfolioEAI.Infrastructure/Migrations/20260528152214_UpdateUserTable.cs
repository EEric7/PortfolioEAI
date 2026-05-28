using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Role_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Role",
                keyValue: null,
                column: "Role",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Role_Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_Id",
                table: "Users",
                column: "Role_Id",
                unique: true);
        }
    }
}
