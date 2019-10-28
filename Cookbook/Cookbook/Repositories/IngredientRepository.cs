using Cookbook.Interfaces;
using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Repositories
{
    public class IngredientRepository : IIngredientRepository 
    {
        private readonly AppDbContext _appDbContext;

        public IngredientRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Ingredient AddIngredient(string name)
        {
            Ingredient ingredient = new Ingredient
            {
                Name = name
            };
            _appDbContext.Ingredients.Add(ingredient);
            _appDbContext.SaveChanges();
            return ingredient;
        }

        public IEnumerable<String> GetAllIngredientName()
        {
            return _appDbContext.Ingredients
                                .Select(i => i.Name);
        }

        public Ingredient GetIngredientByName(string name)
        {
            return _appDbContext.Ingredients
                                .Where(i => i.Name == name)
                                .First();
        }
    }
}
