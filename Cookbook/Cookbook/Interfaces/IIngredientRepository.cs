﻿using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Interfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<String> GetAllIngredientName();
        Ingredient GetIngredientByName(string name);
        Ingredient AddIngredient(string name);
    }
}
