using Microsoft.EntityFrameworkCore.Migrations;

namespace TopKala.DataAccess.Migrations
{
    public partial class AddPhoneNumberCheckToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPhoneNumberConfirmed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPhoneNumberConfirmed",
                table: "Users");
        }
    }
}
