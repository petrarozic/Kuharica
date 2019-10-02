using Cookbook.Interfaces;
using Cookbook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Controllers
{
    [Authorize]
    public class RecipeController: Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [Route("Recipe/{recipeId}")]
        public IActionResult Index(int recipeId)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel()
            {
                Recipe = _recipeRepository.GetRecipeById(recipeId)
            };

            recipeViewModel.Recipe.Steps = recipeViewModel.Recipe.Steps.OrderBy(y => y.Order).ToList();

            return View(recipeViewModel);
        }
    }
}
