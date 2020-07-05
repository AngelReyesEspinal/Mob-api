using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class miversoparecenkilo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Questions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Questions",
                nullable: false,
                defaultValue: 0);
        }
    }
}
