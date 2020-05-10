using Microsoft.EntityFrameworkCore.Migrations;

namespace CaseStudyBooksAPI.Migrations
{
    public partial class w1c2t2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GraphicName",
                table: "Products",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraphicName",
                table: "Products");
        }
    }
}
