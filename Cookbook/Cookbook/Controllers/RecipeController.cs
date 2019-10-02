﻿using Cookbook.DTO;
using Cookbook.Interfaces;
using Cookbook.Models;
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

            recipeViewModel.Recipe.Steps = recipeViewModel.Recipe.Steps.OrderBy(y => y.Order).ToList();

            return View(recipeViewModel);
        }

        public IActionResult NewRecipe()
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            return View(recipeViewModel);
        }

        [HttpPost]
        public IActionResult AddNewRecipe(RecipeViewModel recipeViewModel)
        {
            Recipe recipe = new Recipe();
            recipe.Name = recipeViewModel.Recipe.Name;

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
            foreach(var x in recipeViewModel.Recipe.Steps)
            {
                Step step = new Step()
                {
                    Order = x.Order,
                    Description = x.Description
                };
                recipe.Steps.Add(step);
            }

            _recipeRepository.AddRecipe(recipe);
            return RedirectToAction("Index", "Recipe", new { recipeId = recipe.RecipeId});
        }
    }
}
