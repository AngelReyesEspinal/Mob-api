using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class secretkey22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "Subject",
                newName: "SecretKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecretKey",
                table: "Subject",
                newName: "Identifier");
        }
    }
}
