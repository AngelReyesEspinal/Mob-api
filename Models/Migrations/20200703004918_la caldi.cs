using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class lacaldi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowGifs",
                table: "Evaluations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowGifs",
                table: "Evaluations");
        }
    }
}
