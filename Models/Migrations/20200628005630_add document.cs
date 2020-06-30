using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class adddocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Subject",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<bool>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DocumentId",
                table: "Subject",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_DocumentId",
                table: "Questions",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Document_DocumentId",
                table: "Questions",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Document_DocumentId",
                table: "Subject",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Document_DocumentId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Document_DocumentId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Subject_DocumentId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Questions_DocumentId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Subject",
                nullable: true);
        }
    }
}
