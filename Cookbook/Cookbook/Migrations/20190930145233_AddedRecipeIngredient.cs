using Microsoft.EntityFrameworkCore.Migrations;

namespace Cookbook.Migrations
{
    public partial class AddedRecipeIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeRecipeIngredients_Recipes_RecipeId",
                table: "RecipeRecipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeRecipeIngredients",
                table: "RecipeRecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeRecipeIngredients_RecipeId",
                table: "RecipeRecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "RecipeRecipeIngredients");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeRecipeIngredients");

            migrationBuilder.RenameTable(
                name: "RecipeRecipeIngredients",
                newName: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    MeasuringUnit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "RecipeRecipeIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "RecipeRecipeIngredients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeRecipeIngredients",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeRecipeIngredients",
                table: "RecipeRecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRecipeIngredients_RecipeId",
                table: "RecipeRecipeIngredients",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeRecipeIngredients_Recipes_RecipeId",
                table: "RecipeRecipeIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
