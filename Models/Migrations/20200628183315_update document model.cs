using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class updatedocumentmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalName",
                table: "Document",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Document",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalName",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Document");
        }
    }
}
