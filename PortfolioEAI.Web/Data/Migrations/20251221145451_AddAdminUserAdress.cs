using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioEAI.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUserAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AdminUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AdminUsers",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AdminUsers",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AdminUsers",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "AdminUsers",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "AdminUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AdminUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
