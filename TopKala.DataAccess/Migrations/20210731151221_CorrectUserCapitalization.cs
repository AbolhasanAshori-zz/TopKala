using Microsoft.EntityFrameworkCore.Migrations;

namespace TopKala.DataAccess.Migrations
{
    public partial class CorrectUserCapitalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isImageUploaded",
                table: "Users",
                newName: "IsImageUploaded");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Users",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsImageUploaded",
                table: "Users",
                newName: "isImageUploaded");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "isActive");
        }
    }
}
