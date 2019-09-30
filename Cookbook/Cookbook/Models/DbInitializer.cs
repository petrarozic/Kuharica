using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Recipes.Any())
            {
                var recept0 = new Recipe
                {
                    Name = "Burek",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Meso", Amount = "1kg" }
                    }
                };
                var recept1 = new Recipe
                {
                    Name = "Lazanje",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Meso", Amount = "1kg" },
                        new Ingredient(){ Name = "Tjestenina", Amount = "750g" }
                    }
                };
                var recept2 = new Recipe
                {
                    Name = "Bolonjez",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Meso", Amount = "500g" },
                        new Ingredient(){ Name = "Tjestenina", Amount = "500g" }
                    }
                };
                var recept3 = new Recipe
                {
                    Name = "Grah sa zeljem",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Zelje", Amount = "450g" },
                        new Ingredient(){ Name = "Grah", Amount = "40dag" }
                    }
                };
                var recept4 = new Recipe
                {
                    Name = "Pizza",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Sir", Amount = "25dag" },
                        new Ingredient(){ Name = "Sunka", Amount = "25dag" },
                        new Ingredient(){ Name = "Gljive", Amount = "30dag" }
                    }
                };
                var recept5 = new Recipe
                {
                    Name = "Rižoto",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Meso", Amount = "500g" },
                        new Ingredient(){ Name = "Riza", Amount = "400g" }
                    }
                };
                var recept6 = new Recipe
                {
                    Name = "Granadir",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Tjestenina", Amount = "250g" },
                        new Ingredient(){ Name = "Krumpir", Amount = "250g" }
                    }
                };
                var recept7 = new Recipe
                {
                    Name = "Jaja sa špekom",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Jaja", Amount = "3kom" },
                        new Ingredient(){ Name = "Špek", Amount = "100g" }
                    }
                };
                var recept8 = new Recipe
                {
                    Name = "Hamburger",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Meso", Amount = "250g" },
                        new Ingredient(){ Name = "Sir", Amount = "100g" }
                    }
                };
                var recept9 = new Recipe
                {
                    Name = "Varivo od mahuna",
                    Ingredients = new List<Ingredient>(){
                        new Ingredient(){ Name = "Meso", Amount = "40dag" },
                        new Ingredient(){ Name = "Mahune", Amount = "1kg" }
                    }
                };

                context.Recipes.AddRange(recept0, recept1, recept2, recept3, recept4, recept5, recept6, recept7, recept8, recept9);
                context.SaveChanges();
            }
        }
    }
}
