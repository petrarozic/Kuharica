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
                var recept0 = new Recipe { Name = "Burek" };
                var recept1 = new Recipe { Name = "Lazanje" };
                var recept2 = new Recipe { Name = "Bolonjez" };
                var recept3 = new Recipe { Name = "Grah sa zeljem" };
                var recept4 = new Recipe { Name = "Pizza" };
                var recept5 = new Recipe { Name = "Rižoto" };
                var recept6 = new Recipe { Name = "Granadir" };
                var recept7 = new Recipe { Name = "Jaja sa špekom" };
                var recept8 = new Recipe { Name = "Hamburger" };
                var recept9 = new Recipe { Name = "Varivo od mahuna" };

                context.Recipes.AddRange(recept0, recept1, recept2, recept3, recept4, recept5, recept6, recept7, recept8, recept9);
                context.SaveChanges();
            }
        }
    }
}
