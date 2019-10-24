using Cookbook.DTO;
using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.ViewModels
{
    public class RecipeViewModel
    {
        public RecipeDetailDTO Recipe { get; set; }
        public bool UsersMatch { get; set; }
        public List<AdjustedIngredientDTO> AdjustedIngredients { get; set; }
    }
}
