using Microsoft.EntityFrameworkCore.Migrations;

namespace TopKala.DataAccess.Migrations
{
    public partial class AddNewsletterAndForeignToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNewsletterSubscripted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserForeign",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNewsletterSubscripted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsUserForeign",
                table: "Users");
        }
    }
}
