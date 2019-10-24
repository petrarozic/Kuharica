using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.DTO;
using Cookbook.Enums;
using Cookbook.Interfaces;
using Cookbook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookbook.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public HomeController(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Recipes = _recipeRepository.GetAllRecipe()
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
        public IActionResult Search(string searchByName, List<IngredientDTO> searchByIngredients)
        {
            searchByIngredients.RemoveAll(x => String.IsNullOrWhiteSpace(x.Name));

            List<string> ingredientNameForSearch = new List<string>();
            foreach (var x in searchByIngredients)
            {
                ingredientNameForSearch.Add(x.Name);
            }

            var homeViewModel = new HomeViewModel()
            {
                Recipes = _recipeRepository.SearchRecipe(searchByName, ingredientNameForSearch)
                                            .OrderBy(r => r.Name)
                                            .Select(u => new RecipeDTO
                                            {
                                                Id = u.RecipeId,
                                                Name = u.Name
                                            })
                                            .ToList(),
                searchByIngredients = searchByIngredients
            };

            return View("Index", homeViewModel);
        }

        [HttpGet]
        public JsonResult getOfferedIngredients()
        {
            List<string> offeredIngredients = new List<string>();
            var ingredients = _ingredientRepository.GetAllIngredient().ToList();
            foreach (var i in ingredients)
            {
                offeredIngredients.Add(i.Name);
            }

            return Json(offeredIngredients);
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
    }
}
