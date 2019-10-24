using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.DTO;
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

        public HomeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
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
    }
}
