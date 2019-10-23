using Cookbook.DTO;
using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.ViewModels
{
    public class HomeViewModel
    {
        public List<RecipeDTO> Recipes { get; set; }
        public string searchByName { get; set; }
        public List<IngredientDTO> searchByIngredients { get; set; }
    }
}
