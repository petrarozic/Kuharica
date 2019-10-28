﻿using Cookbook.Interfaces;
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
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeRepository(AppDbContext appDbContext, IIngredientRepository ingredientRepository)
        {
            _appDbContext = appDbContext;
            _ingredientRepository = ingredientRepository;
        }

        public void AddRecipe(Recipe recipe)
        {
            List<RecipeIngredient> recipeIngredients = recipe.RecipeIngredients.ToList();
            recipe.RecipeIngredients = new List<RecipeIngredient>();
            List<String> ingredientNames = _ingredientRepository.GetAllIngredientName().ToList();
            foreach (var x in recipeIngredients)
            {
                if (ingredientNames.Contains(x.Ingredient.Name))
                {
                    recipe.RecipeIngredients.Add(
                        new RecipeIngredient
                        {
                            Ingredient = _ingredientRepository.GetIngredientByName(x.Ingredient.Name),
                            Amount = x.Amount,
                            MeasuringUnit = x.MeasuringUnit
                        });
                }
                else
                {
                    Ingredient ingredient = _ingredientRepository.AddIngredient(x.Ingredient.Name);
                    recipe.RecipeIngredients.Add(
                        new RecipeIngredient
                        {
                            Ingredient = ingredient,
                            Amount = x.Amount,
                            MeasuringUnit = x.MeasuringUnit
                        });
                }
            }

            _appDbContext.Recipes.Add(recipe);
            _appDbContext.SaveChanges();
        }

        public void DeleteRecipe(int recipeId)
        {
            Recipe recipe = GetRecipeById(recipeId);
            _appDbContext.Remove(recipe);
            _appDbContext.SaveChanges();
        }

        public void EditRecipe(Recipe recipe)
        {
            var currentRecipe = GetRecipeById(recipe.RecipeId);
            currentRecipe.Name = recipe.Name;
            currentRecipe.Public = recipe.Public;
            currentRecipe.Steps = recipe.Steps;
            currentRecipe.RecipeIngredients = recipe.RecipeIngredients;
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Recipe> GetAllRecipe()
        {
            return _appDbContext.Recipes
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps)
                                .Include(r => r.ApplicationUser);
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _appDbContext.Recipes
                                .Where(r => r.RecipeId == recipeId)
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps)
                                .Include(r => r.ApplicationUser)
                                .First();
        }

        public IEnumerable<Recipe> SearchRecipe(string searchByName, List<string> searchByIngredients)
        {
            if (String.IsNullOrEmpty(searchByName) && ListIsNullOrEmpty(searchByIngredients)) return GetAllRecipe();
            if (!String.IsNullOrEmpty(searchByName) && ListIsNullOrEmpty(searchByIngredients)) return GetAllRecipeByName(searchByName);
            if (String.IsNullOrEmpty(searchByName) && !ListIsNullOrEmpty(searchByIngredients)) return GetAllRecipeByIngredients(searchByIngredients);

            return _appDbContext.Ingredients
                                .Where(i => CaseInsensitiveContains(i.Name, searchByIngredients))
                                .SelectMany(i => i.RecipeIngredients)
                                .Select(ri => ri.Recipe)
                                .Distinct()
                                .Where(r => CaseInsensitiveContains(r.Name, searchByName))
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps)
                                .Include(r => r.ApplicationUser);
        }

        private IEnumerable<Recipe> GetAllRecipeByName(string searchByName)
        {
            return _appDbContext.Recipes
                                .Where(r => CaseInsensitiveContains(r.Name, searchByName))
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps)
                                .Include(r => r.ApplicationUser);
        }

        private IEnumerable<Recipe> GetAllRecipeByIngredients(List<string> searchByIngredients)
        {
            return _appDbContext.Ingredients
                                .Where(i => CaseInsensitiveContains(i.Name, searchByIngredients))
                                .SelectMany(i => i.RecipeIngredients)
                                .Distinct()
                                .Select(ri => ri.Recipe)
                                .Distinct()
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps)
                                .Include(r => r.ApplicationUser);
        }

        private bool ListIsNullOrEmpty(List<string> list)
        {
            if (list == null) return true;
            if (list.Count == 0) return true;
            if (list[0] == null) return true;
            return !list.Any();
        }

        private bool CaseInsensitiveContains(string name, List<string> searchedName)
        {
            foreach (var x in searchedName)
            {
                if (name.ToUpper().Contains(x.ToUpper())) return true;
            }
            return false;
        }

        private bool CaseInsensitiveContains(string name, string searchedName)
        {
            return name.ToUpper().Contains(searchedName.ToUpper());
        }
    }
}
