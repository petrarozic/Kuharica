﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public ICollection<Step> Steps { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public bool Public { get; set; }
    }
}
