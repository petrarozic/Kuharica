using Cookbook.DTO;
using Cookbook.Interfaces;
using Cookbook.Models;
using Cookbook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipeController(IRecipeRepository recipeRepository, UserManager<ApplicationUser> userManager)
        {
            _recipeRepository = recipeRepository;
            _userManager = userManager;
        }

        public IActionResult Index(int recipeId)
        {
            Recipe recipe = _recipeRepository.GetRecipeById(recipeId);

            RecipeViewModel recipeViewModel = new RecipeViewModel()
            {
                Recipe = new DTO.RecipeDetailDTO()
                {
                    Name = recipe.Name,
                    RecipeId = recipe.RecipeId,
                }
            };

            List<IngredientDTO> ingredientDTOs = new List<IngredientDTO>(); 
            foreach(var x in recipe.RecipeIngredients)
            {
                IngredientDTO ingredientDTO = new IngredientDTO()
                {
                    Name = x.Ingredient.Name,
                    Amount = x.Amount,
                    MeasuringUnit = x.MeasuringUnit
                };
                ingredientDTOs.Add(ingredientDTO);
            }
            recipeViewModel.Recipe.Ingredients = ingredientDTOs;

            List<StepDTO> stepDTOs = new List<StepDTO>();
            foreach(var x in recipe.Steps)
            {
                StepDTO stepDTO = new StepDTO()
                {
                    Order = x.Order,
                    Description = x.Description
                };
                stepDTOs.Add(stepDTO);
            }
            recipeViewModel.Recipe.Steps = stepDTOs.OrderBy(y => y.Order).ToList();

            recipeViewModel.Recipe.UserEmail = recipe.ApplicationUser.Email;

            return View(recipeViewModel);
        }

        public IActionResult NewRecipe()
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            return View(recipeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRecipe(RecipeViewModel recipeViewModel)
        {
            Recipe recipe = new Recipe();
            recipe.Name = recipeViewModel.Recipe.Name;
            recipe.ApplicationUser = await _userManager.GetUserAsync(HttpContext.User);

            recipe.RecipeIngredients = new List<RecipeIngredient>();
            foreach (var x in recipeViewModel.Recipe.Ingredients)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient()
                {
                    Ingredient = new Ingredient() { Name = x.Name },
                    Amount = x.Amount,
                    MeasuringUnit = x.MeasuringUnit
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }

            recipe.Steps = new List<Step>();
            int orderNum = 0;
            foreach (var x in recipeViewModel.Recipe.Steps)
            {
                orderNum++;
                Step step = new Step()
                {
                    Order = orderNum,
                    Description = x.Description
                };
                recipe.Steps.Add(step);
            }

            _recipeRepository.AddRecipe(recipe);
            return RedirectToAction("Index", "Recipe", new { recipeId = recipe.RecipeId});
        }

        [Route("Recipe/EditRecipe/{recipeId}")]
        public IActionResult EditRecipe(int recipeId)
        {
            return View();
        }
    }
}
