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
                context.Ingredients.AddRange(
                    new Ingredient { Name = "Meso" },
                    new Ingredient { Name = "Mahune" },
                    new Ingredient { Name = "Sir" },
                    new Ingredient { Name = "Zelje" },
                    new Ingredient { Name = "Sunka" },
                    new Ingredient { Name = "Jaja" },
                    new Ingredient { Name = "Špek" },
                    new Ingredient { Name = "Tjestenina" },
                    new Ingredient { Name = "Krumpir" },
                    new Ingredient { Name = "Riža" },
                    new Ingredient { Name = "Gljive" },
                    new Ingredient { Name = "Grah" }
                    );
                context.SaveChanges();

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

                recept0.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 1,
                        MeasuringUnit = "kg"
                    },
                };

                recept1.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 1,
                        MeasuringUnit = "kg"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 750,
                        MeasuringUnit = "g"
                    }
                };

                recept2.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 500,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 500,
                        MeasuringUnit = "g"
                    }
                };

                recept3.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Zelje"),
                        Amount = 450,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Grah"),
                        Amount = 40,
                        MeasuringUnit = "dag"
                    }
                };

                recept4.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sir"),
                        Amount = 25,
                        MeasuringUnit = "dag"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sunka"),
                        Amount = 25,
                        MeasuringUnit = "dag"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Gljive"),
                        Amount = 30,
                        MeasuringUnit = "dag"
                    }
                };

                recept5.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 500,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Riža"),
                        Amount = 400,
                        MeasuringUnit = "g"
                    }
                };

                recept6.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 250,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Krumpir"),
                        Amount = 250,
                        MeasuringUnit = "g"
                    }
                };

                recept7.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Jaja"),
                        Amount = 3,
                        MeasuringUnit = "kom"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Špek"),
                        Amount = 100,
                        MeasuringUnit = "g"
                    }
                };

                recept8.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 250,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sir"),
                        Amount = 100,
                        MeasuringUnit = "g"
                    }
                };

                recept9.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient { Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 40,
                        MeasuringUnit = "dag"
                    },
                    new RecipeIngredient { Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Mahune"),
                        Amount = 1,
                        MeasuringUnit = "kg"
                    }
                };

                context.SaveChanges();

                recept0.Procedure = "Razvaljas tijesto. Razbacas meso. Zarolas i stavis pec.";
                recept1.Procedure = "Postavis tijesto. Stavis meso. Ponavljas postupak. Stavis peć.";
                recept2.Procedure = "Izdinstas meso sa lukom. Skuhas tijesteninu i dodas je.";
                recept3.Procedure = "Prokuhas grah. Dodas zelje i kuhas jos neko vrijeme.";
                recept4.Procedure = "Rastegnes tijesto. Poslozis sunku. Poslozis sir. Poslozis gljive. Stavis pec.";
                recept5.Procedure = "Izdinstas luk i meso. Dodas rizu i kuhas jos 15min.";
                recept6.Procedure = "Skuhas krumpir. Skuhas tijesto. Pomijesas zajedno krumpir i tijesto.";
                recept7.Procedure = "Nasijeckas spek. Sprzis spek na ulju. Dodas razmucena jaja i speces.";
                recept8.Procedure = "Prerezes pecivo. Dodas peceno meso. Dodas sir.";
                recept9.Procedure = "Prodinstas luk sa mesom. Dodas mahune i kuhas jos neko vrijeme.";

                context.SaveChanges();
            }
        }
    }
}
