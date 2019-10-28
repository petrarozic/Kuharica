using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.DTO;
using Cookbook.Enums;
using Cookbook.Interfaces;
using Cookbook.Models;
using Cookbook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookbook.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository, UserManager<ApplicationUser> userManager)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Recipes = _recipeRepository.GetAllRecipe(applicationUser.Id)
                                            .OrderBy(r => r.Name)
                                            .Select(u => new RecipeDTO
                                            {
                                                Id = u.RecipeId,
                                                Name = u.Name
                                            })
                                            .ToList(),
                searchByIngredients = new List<IngredientDTO>()
            };

            return View(homeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchByName, List<IngredientDTO> searchByIngredients)
        {
            searchByIngredients.RemoveAll(x => String.IsNullOrWhiteSpace(x.Name));

            List<string> ingredientNameForSearch = new List<string>();
            foreach (var x in searchByIngredients)
            {
                ingredientNameForSearch.Add(x.Name);
            }

            var homeViewModel = new HomeViewModel()
            {
                //Recipes = _recipeRepository.SearchRecipe(searchByName, ingredientNameForSearch)
                //                            .OrderBy(r => r.Name)
                //                            .Select(u => new RecipeDTO
                //                            {
                //                                Id = u.RecipeId,
                //                                Name = u.Name
                //                            })
                //                            .ToList(),
                searchByIngredients = searchByIngredients
            };

            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            var tempRecipes = _recipeRepository.SearchRecipe(searchByName, ingredientNameForSearch, applicationUser.Id)
                                                        .OrderBy(r => r.Name);
            homeViewModel.Recipes = SortRecipe(tempRecipes, ingredientNameForSearch); 

            return View("Index", homeViewModel);
        }

        [HttpGet]
        public JsonResult getOfferedIngredients()
        {
            return Json(_ingredientRepository.GetAllIngredientName().ToList());
        }

        [HttpGet]
        public JsonResult getOfferedMeasuringUnits()
        {
            List<MeasuringUnitDTO> measuringUnitDTOs = new List<MeasuringUnitDTO>();
            foreach (var x in Enum.GetValues(typeof(MeasuringUnitType)))
            {
                measuringUnitDTOs.Add(new MeasuringUnitDTO()
                {
                    Value = (int)x,
                    Name = x.ToString()
                });
            }
            return Json(measuringUnitDTOs);
        }

        private List<RecipeDTO> SortRecipe(IEnumerable<Recipe> recipes, List<string> ingredientNameForSearch)
        {
            List<RecipeDTO> sortedList = new List<RecipeDTO>();

            int maxNum = 0;
            foreach (var x in recipes)
            {
                int tempNum = HowManyWantedIngredients(x, ingredientNameForSearch);
                if (maxNum < tempNum) maxNum = tempNum;
            }

            for (int i = maxNum; i > -1; i--)
            {
                foreach (var x in recipes.ToList())
                {
                    int tempNum = HowManyWantedIngredients(x, ingredientNameForSearch);
                    if (i == tempNum)
                    {
                        var recipe = new RecipeDTO()
                        {
                            Id = x.RecipeId,
                            Name = x.Name
                        };
                        sortedList.Add(recipe);
                        recipes.ToList().Remove(x);
                    }
                }
            }
            return sortedList;
        }

        private int HowManyWantedIngredients(Recipe recipe, List<string> ingredientNameForSearch)
        {
            int num = 0;
            foreach (var x in recipe.RecipeIngredients)
            {
                if (ingredientNameForSearch.Contains(x.Ingredient.Name)) num++;
            }
            return num;
        }
    }
}
