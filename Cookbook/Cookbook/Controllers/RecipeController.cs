using Cookbook.DTO;
using Cookbook.Interfaces;
using Cookbook.Models;
using Cookbook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public async Task<IActionResult> Index(int recipeId, string searchIngredients)
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

            //kreiram listu IngredientDTO po kojima se pretrazuje
            List<IngredientDTO> searchedIngredients = new List<IngredientDTO>();
            if (!String.IsNullOrEmpty(searchIngredients))
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                searchedIngredients = JsonConvert.DeserializeObject<List<IngredientDTO>>(searchIngredients, settings);
            }

            //kopiram sastojke iz orginalnog recepta
            List<AdjustedIngredientDTO> adjustedIngredients = new List<AdjustedIngredientDTO>();
            foreach (var x in recipeViewModel.Recipe.Ingredients)
            {
                AdjustedIngredientDTO adjustedIngredient = new AdjustedIngredientDTO()
                {
                    Name = x.Name,
                    Amount = (double)x.Amount,
                    MeasuringUnit = x.MeasuringUnit
                };
                adjustedIngredients.Add(adjustedIngredient);
            }

            //ima li zadane kolicine i mjerne jedinice 
            bool percentageExist = false;
            if (searchedIngredients.Any())
            {
                double percentage = 0;
                foreach (var x in searchedIngredients)
                {
                    if (x.Amount > 0)
                    {
                        var tempIngredinet = adjustedIngredients.Find(r => r.Name == x.Name);
                        if (IsComparableMeasuringUnits(x.MeasuringUnit, tempIngredinet.MeasuringUnit))
                        {
                            double tempPercentage = 0;
                            tempPercentage = ConvertToSpecificMeasuringUnit(x.MeasuringUnit, x.Amount) /
                                            ConvertToSpecificMeasuringUnit(tempIngredinet.MeasuringUnit, tempIngredinet.Amount);
                            if (!percentageExist && tempPercentage != 1)
                            {
                                percentageExist = true;
                                percentage = tempPercentage;
                            }
                            else if (percentage > tempPercentage) percentage = tempPercentage;
                        }
                    }
                }
                if (percentageExist)
                {
                    foreach (var x in adjustedIngredients)
                    {
                        x.Amount = (double)x.Amount * percentage;
                    }
                }
            }

            if (percentageExist) recipeViewModel.AdjustedIngredients = adjustedIngredients;
            //no need to display adjustedRecipe
            else recipeViewModel.AdjustedIngredients = new List<AdjustedIngredientDTO>();

            return View(recipeViewModel);
        }

        private bool IsComparableMeasuringUnits(Enums.MeasuringUnitType measuringUnit1, Enums.MeasuringUnitType measuringUnit2)
        {
            List<Enums.MeasuringUnitType> weightMU = new List<Enums.MeasuringUnitType>(){
                Enums.MeasuringUnitType.g,
                Enums.MeasuringUnitType.dag,
                Enums.MeasuringUnitType.kg
            };
            List<Enums.MeasuringUnitType> volumeMU = new List<Enums.MeasuringUnitType>() {
                Enums.MeasuringUnitType.mL,
                Enums.MeasuringUnitType.L,
                Enums.MeasuringUnitType.dL
            };
            List<Enums.MeasuringUnitType> piecesMU = new List<Enums.MeasuringUnitType>() {
                Enums.MeasuringUnitType.kom
            };

            if (weightMU.Contains(measuringUnit1) && weightMU.Contains(measuringUnit2)) return true;
            if (volumeMU.Contains(measuringUnit1) && volumeMU.Contains(measuringUnit2)) return true;
            if (piecesMU.Contains(measuringUnit1) && piecesMU.Contains(measuringUnit2)) return true;

            return false;
        }

        private double ConvertToSpecificMeasuringUnit(Enums.MeasuringUnitType measuringUnit, double amount)
        {
            switch (measuringUnit)
            {
                case Enums.MeasuringUnitType.kg:
                    return amount * 100;
                case Enums.MeasuringUnitType.g:
                    return amount * 0.1;
                case Enums.MeasuringUnitType.kom:
                    return amount;
                case Enums.MeasuringUnitType.L:
                    return amount * 10;
                case Enums.MeasuringUnitType.mL:
                    return amount * 0.01;
            }
            return amount;
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
        public async Task<IActionResult> EditRecipe(int recipeId)
        {
            Recipe recipe = _recipeRepository.GetRecipeById(recipeId);
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            if (recipe.ApplicationUser.Id != applicationUser.Id)
            {
                ViewBag.Error = "You do not have permission to edit this recipe";
                return View("Warning");
            }

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
        public async Task<IActionResult> EditRecipe(RecipeViewModel recipeViewModel)
        {
            var userWhoCreatedRecipe = _recipeRepository.GetRecipeById(recipeViewModel.Recipe.RecipeId).ApplicationUser;
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            if (userWhoCreatedRecipe.Id != applicationUser.Id)
            {
                ViewBag.Error = "You do not have permission to edit this recipe";
                return View("Warning");
            }

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

        [Route("Recipe/DeleteRecipe/{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            Recipe recipe = _recipeRepository.GetRecipeById(recipeId);
            if (recipe.ApplicationUser.Id != applicationUser.Id)
            {
                ViewBag.Error = "You do not have permission to delete this recipe";
                return View("Warning");
            }

            _recipeRepository.DeleteRecipe(recipeId);
            return RedirectToAction("Index", "Home");
        }
    }
}
