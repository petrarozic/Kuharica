using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public static class DbInitializer
    {
        public static async Task Seed(AppDbContext context, UserManager<ApplicationUser> _userManager)
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
                        MeasuringUnit = Enums.MeasuringUnitType.kg
                    },
                };

                recept1.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 1,
                        MeasuringUnit = Enums.MeasuringUnitType.kg
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 750,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    }
                };

                recept2.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 500,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 500,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    }
                };

                recept3.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Zelje"),
                        Amount = 450,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Grah"),
                        Amount = 40,
                        MeasuringUnit = Enums.MeasuringUnitType.dag
                    }
                };

                recept4.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sir"),
                        Amount = 25,
                        MeasuringUnit = Enums.MeasuringUnitType.dag
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sunka"),
                        Amount = 25,
                        MeasuringUnit = Enums.MeasuringUnitType.dag
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Gljive"),
                        Amount = 30,
                        MeasuringUnit = Enums.MeasuringUnitType.dag
                    }
                };

                recept5.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 500,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Riža"),
                        Amount = 400,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    }
                };

                recept6.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 250,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Krumpir"),
                        Amount = 250,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    }
                };

                recept7.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Jaja"),
                        Amount = 3,
                        MeasuringUnit = Enums.MeasuringUnitType.kom
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Špek"),
                        Amount = 100,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    }
                };

                recept8.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 250,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sir"),
                        Amount = 100,
                        MeasuringUnit = Enums.MeasuringUnitType.g
                    }
                };

                recept9.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient { Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 40,
                        MeasuringUnit = Enums.MeasuringUnitType.dag
                    },
                    new RecipeIngredient { Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Mahune"),
                        Amount = 1,
                        MeasuringUnit = Enums.MeasuringUnitType.kg
                    }
                };

                context.SaveChanges();

                recept0.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Razvaljas tijesto" },
                        new Step { Order = 2, Description = "Razbacas meso" },
                        new Step { Order = 3, Description = "Zarolas i stavis pec" }
                    };
                recept1.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Postavis tijesto" },
                        new Step { Order = 2, Description = "Stavis meso" },
                        new Step { Order = 3, Description = "Ponavljas postupak" },
                        new Step { Order = 4, Description = "Stavis peć" }
                    };
                recept2.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Izdinstas meso sa lukom" },
                        new Step { Order = 2, Description = "Skuhas tijesteninu i dodas je" }
                    };
                recept3.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Prokuhas grah" },
                        new Step { Order = 2, Description = "Dodas zelje i kuhas jos neko vrijeme" }
                    };
                recept4.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Rastegnes tijesto" },
                        new Step { Order = 2, Description = "Poslozis sunku" },
                        new Step { Order = 3, Description = "Poslozis sir" },
                        new Step { Order = 4, Description = "Poslozis gljive" },
                        new Step { Order = 5, Description = "Stavis pec" }
                    };
                recept5.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Izdinstas luk i meso" },
                        new Step { Order = 2, Description = "Dodas rizu i kuhas jos 15min" }
                    };
                recept6.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Skuhas krumpir" },
                        new Step { Order = 2, Description = "Skuhas tijesto" },
                        new Step { Order = 3, Description = "Pomijesas zajedno krumpir i tijesto" }
                    };
                recept7.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Nasijeckas spek" },
                        new Step { Order = 2, Description = "Sprzis spek na ulju" },
                        new Step { Order = 3, Description = "Dodas razmucena jaja i speces" }
                    };
                recept8.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Prerezes pecivo" },
                        new Step { Order = 2, Description = "Dodas peceno meso" },
                        new Step { Order = 3, Description = "Dodas sir" }
                    };
                recept9.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Prodinstas luk sa mesom" },
                        new Step { Order = 2, Description = "Dodas mahune i kuhas jos neko vrijeme" }
                    };

                context.SaveChanges();

                ApplicationUser applicationUser1 = new ApplicationUser()
                {
                    UserName = "user1@user.user",
                    Email = "user1@user.user"
                };
                await _userManager.CreateAsync(applicationUser1, "User123!");

                ApplicationUser applicationUser2 = new ApplicationUser()
                {
                    UserName = "user2@user.user",
                    Email = "user2@user.user"
                };
                await _userManager.CreateAsync(applicationUser2, "User123!");

                recept0.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept1.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept2.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept3.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept4.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept5.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept6.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept7.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept8.ApplicationUser = RandomUser(applicationUser1, applicationUser2);
                recept9.ApplicationUser = RandomUser(applicationUser1, applicationUser2);

                context.SaveChanges();

                recept0.Public = RandomPublic();
                recept1.Public = RandomPublic();
                recept2.Public = RandomPublic();
                recept3.Public = RandomPublic();
                recept4.Public = RandomPublic();
                recept5.Public = RandomPublic();
                recept6.Public = RandomPublic();
                recept7.Public = RandomPublic();
                recept8.Public = RandomPublic();
                recept9.Public = RandomPublic();

                context.SaveChanges();
            }
        }

        private static ApplicationUser RandomUser(ApplicationUser applicationUser1, ApplicationUser applicationUser2)
        {
            Random random = new Random();
            switch (random.Next(1, 3))
            {
                case 1:
                    return applicationUser1;
                case 2:
                    return applicationUser2;
            }
            return null;
        }

        private static bool RandomPublic()
        {
            Random random = new Random();
            switch (random.Next(1, 3))
            {
                case 1:
                    return true;
            }
            return false;
        }
    }
}
