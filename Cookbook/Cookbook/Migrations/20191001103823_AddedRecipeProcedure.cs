using Microsoft.EntityFrameworkCore.Migrations;

namespace Cookbook.Migrations
{
    public partial class AddedRecipeProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Procedure",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Procedure",
                table: "Recipes");
        }
    }
}
