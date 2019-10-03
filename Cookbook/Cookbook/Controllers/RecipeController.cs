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

        public async Task<IActionResult> Index(int recipeId)
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

            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            if(recipe.ApplicationUser.Id == applicationUser.Id)
            {
                recipeViewModel.UsersMatch = true;
            } else
            {
                recipeViewModel.UsersMatch = false;
            }

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
            Recipe recipe = _recipeRepository.GetRecipeById(recipeId);

            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel.Recipe = new RecipeDetailDTO()
            {
                RecipeId = recipe.RecipeId
            };

            return View(recipeViewModel);
        }

        [HttpGet]
        public JsonResult GetRecipeData(int recipeId)
        {
            Recipe recipe = _recipeRepository.GetRecipeById(recipeId);

            RecipeViewModel recipeViewModel = new RecipeViewModel()
            {
                Recipe = new RecipeDetailDTO()
                {
                    RecipeId = recipe.RecipeId,
                    Name = recipe.Name,
                    Ingredients = new List<IngredientDTO>(),
                    Steps = new List<StepDTO>()
                }
            };

            foreach (var x in recipe.RecipeIngredients)
            {
                IngredientDTO searchIngredient = new IngredientDTO()
                {
                    Name = x.Ingredient.Name,
                    Amount = x.Amount,
                    MeasuringUnit = x.MeasuringUnit
                };
                recipeViewModel.Recipe.Ingredients.Add(searchIngredient);
            }

            foreach (var x in recipe.Steps)
            {
                StepDTO searchStep = new StepDTO()
                {
                    Order = x.Order,
                    Description = x.Description
                };
                recipeViewModel.Recipe.Steps.Add(searchStep);
            }
            recipeViewModel.Recipe.Steps = recipeViewModel.Recipe.Steps.OrderBy(y => y.Order).ToList();

            return Json(recipeViewModel);
        }

        [HttpPost]
        [Route("Recipe/EditRecipe/{recipeId}")]
        public IActionResult EditRecipe(RecipeViewModel recipeViewModel)
        {
            Recipe updatedRecipe = new Recipe();
            updatedRecipe.RecipeId = recipeViewModel.Recipe.RecipeId;
            updatedRecipe.Name = recipeViewModel.Recipe.Name;

            updatedRecipe.RecipeIngredients = new List<RecipeIngredient>();
            foreach (var x in recipeViewModel.Recipe.Ingredients)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient
                {
                    Ingredient = new Ingredient { Name = x.Name },
                    Amount = x.Amount,
                    MeasuringUnit = x.MeasuringUnit
                };
                updatedRecipe.RecipeIngredients.Add(recipeIngredient);
            }

            updatedRecipe.Steps = new List<Step>();
            int orderNum = 0;
            foreach (var s in recipeViewModel.Recipe.Steps)
            {
                orderNum++;
                Step step = new Step()
                {
                    Order = orderNum,
                    Description = s.Description
                };
                updatedRecipe.Steps.Add(step);
            }

            _recipeRepository.EditRecipe(updatedRecipe);

            return RedirectToAction("Index", "Recipe", new { recipeId = updatedRecipe.RecipeId});
        }
    }
}
