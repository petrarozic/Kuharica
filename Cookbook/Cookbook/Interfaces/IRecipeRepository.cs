using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Interfaces
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAllRecipe(string userId);
        Recipe GetRecipeById(int recipeId);
        void AddRecipe(Recipe recipe);
        void EditRecipe(Recipe updatedRecipe);
        void DeleteRecipe(int recipeId);
        IEnumerable<Recipe> SearchRecipe(string searchByName, List<string> searchByIngredient, string userId);
    }
}
