using Microsoft.EntityFrameworkCore.Migrations;

namespace TopKala.DataAccess.Migrations
{
    public partial class AddImageUploadCheckToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isImageUploaded",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isImageUploaded",
                table: "Users");
        }
    }
}
