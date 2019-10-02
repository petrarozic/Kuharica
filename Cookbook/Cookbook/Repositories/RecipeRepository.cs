using Cookbook.Interfaces;
using Cookbook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _appDbContext;

        public RecipeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddRecipe(Recipe recipe)
        {
            _appDbContext.Recipes.Add(recipe);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Recipe> GetAllRecipe()
        {
            return _appDbContext.Recipes
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps);
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _appDbContext.Recipes
                                .Where(r => r.RecipeId == recipeId)
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps)
                                .First();
        }
    }
}
