using GiveMeARecipe.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveMeARecipe.Data
{
    public static class Initializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            var categories = new[]
            {
                new Category { Name = "Appetizers & Snacks" },
                new Category { Name = "Beverages" },
                new Category { Name = "Breakfast" },
                new Category { Name = "Dessert" },
                new Category { Name = "Dips & Dressings" },
                new Category { Name = "Main Dishes" },
                new Category { Name = "Salads" },
                new Category { Name = "Side Dishes" },
                new Category { Name = "Slow Cooker" },
                new Category { Name = "Soups & Stews" }
            };

            context.Set<Category>().AddRange(categories);
            await context.SaveChangesAsync();

            // ------------------------------------------------------------

            var ingredients = new[]
            {
                new Ingredient { Name = "Apple" },
                new Ingredient { Name = "Egg" },
                new Ingredient { Name = "Onion" },
                new Ingredient { Name = "Rice" },
                new Ingredient { Name = "Tomato" },
                new Ingredient { Name = "Milk" },
                new Ingredient { Name = "Pretzel Miniature" },
                new Ingredient { Name = "Peanuts" },
                new Ingredient { Name = "Bacon" },
                new Ingredient { Name = "Olive Oil" },
                new Ingredient { Name = "Chicken" },
                new Ingredient { Name = "Butter" },
                new Ingredient { Name = "Grated Cheese" },
                new Ingredient { Name = "Hot Dog" },
                new Ingredient { Name = "Popcorn" },
                new Ingredient { Name = "Pepper" },
                new Ingredient { Name = "Saltine Cracker" },
                new Ingredient { Name = "Garlic" },
                new Ingredient { Name = "Cumin" }
            };

            context.Set<Ingredient>().AddRange(ingredients);
            await context.SaveChangesAsync();

            // ------------------------------------------------------------

            var recipes = new[]
            {
                new Recipe
                {
                    CategoriesRecipes = categories.Where(c => c.Name == "Breakfast").Select(c => new CategoryRecipe { Category = c }).ToList(),
                    Components = new List<RecipeComponent>
                    {
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Egg").First().IngredientId,
                            Size = 6,
                            Unit = RecipeComponent.UnitOfMeasure.Unit
                        }
                    },
                    Description = "Boiled eggs are eggs, typically from a chicken, cooked with their shells unbroken, usually by immersion in boiling water. Hard-boiled eggs are cooked so that the egg white and egg yolk both solidify, while soft-boiled eggs may leave the yolk, and sometimes the white, at least partially liquid and raw.",
                    Difficulty = 1,
                    Energy = 462,
                    Name = "Hard Boiled Eggs",
                    Picture = "hard-boiled-eggs.jpg",
                    Procedure = new List<Step>
                    {
                        new Step
                        {
                            Index = 1,
                            Instruction = "Place eggs in a saucepan and cover with water at least 1/2″ above the eggs.",
                        },
                        new Step
                        {
                            Index = 2,
                            Instruction = "Bring water to a rolling boil over high heat. Cover and remove from heat.",
                        },
                        new Step
                        {
                            Index = 3,
                            Instruction = "Let stand covered for 15-17 minutes (for large eggs).",
                        },
                        new Step
                        {
                            Index = 4,
                            Instruction = "Remove from hot water and place in a bowl of ice water or run under cold water for 5 minutes.",
                        }
                    },
                    Servings = 6,
                    Time = 18
                },
                new Recipe
                {
                    CategoriesRecipes = categories.Where(c => c.Name == "Breakfast").Select(c => new CategoryRecipe { Category = c }).ToList(),
                    Components = new List<RecipeComponent>
                    {
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Egg").First().IngredientId,
                            Size = 3,
                            Unit = RecipeComponent.UnitOfMeasure.Unit
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Bacon").First().IngredientId,
                            Size = 6,
                            Unit = RecipeComponent.UnitOfMeasure.Unit
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Pepper").First().IngredientId,
                            Size = 1,
                            Unit = RecipeComponent.UnitOfMeasure.Unit
                        }
                    },
                    Description = "This is a great breakfast, especially on the weekend. You fry bacon on a skillet, then after the bacon is crispy, fry eggs in the bacon grease. Garnish your plate with toast or fruit.",
                    Difficulty = 1,
                    Energy = 328,
                    Name = "Heart Attack Eggs",
                    Picture = "heart-attack-eggs.jpg",
                    Procedure = new List<Step>
                    {
                        new Step
                        {

                            Index = 1,
                            Instruction = "Fry the bacon in a large skillet over medium heat until crisp.",
                        },
                        new Step
                        {
                            Index = 2,
                            Instruction = "Remove from the pan, and set on paper towels to drain.",
                        },
                        new Step
                        {
                            Index = 3,
                            Instruction = "Crack the eggs into the pan with the bacon grease so that they are about 1 inch apart.",
                        },
                        new Step
                        {
                            Index = 4,
                            Instruction = "Season with salt and pepper. When the eggs look firm, flip them over, and cook on the other side to your desired doneness.",
                        },
                        new Step
                        {
                            Index = 5,
                            Instruction = "Transfer to a plate and serve with bacon..",
                        }
                    },
                    Servings = 3,
                    Time = 32
                },
                new Recipe
                {
                    CategoriesRecipes = categories.Where(c => c.Name == "Appetizers & Snacks").Select(c => new CategoryRecipe { Category = c }).ToList(),
                    Components = new List<RecipeComponent>
                    {
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Bacon").First().IngredientId,
                            Size = 16,
                            Unit = RecipeComponent.UnitOfMeasure.Piece
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Hot Dog").First().IngredientId,
                            Size = 4,
                            Unit = RecipeComponent.UnitOfMeasure.Unit
                        },
                    },
                    Description = "Hot dogs wrapped in smoky bacon? You know these appetizers are going to be a hit, even if you've never made them before.",
                    Difficulty = 1,
                    Energy = 110,
                    Name = "Smoky Bacon-Wrapped Hot Dog Appetizer Bites",
                    Picture = "smoky-bacon-hot-dog.jpg",
                    Procedure = new List<Step>
                    {
                        new Step
                        {

                            Index = 1,
                            Instruction = "Heat oven to 400ºF.",
                        },
                        new Step
                        {
                            Index = 2,
                            Instruction = "Wrap bacon around frank pieces; secure with wooden toothpicks.",
                        },
                        new Step
                        {
                            Index = 3,
                            Instruction = "Place on rimmed baking sheet.",
                        },
                        new Step
                        {
                            Index = 4,
                            Instruction = "Bake 25 min. or until bacon is crisp, turning after 15 min.",
                        }
                    },
                    Servings = 16,
                    Time = 35
                },
                new Recipe
                {
                    CategoriesRecipes = categories.Where(c => c.Name == "Appetizers & Snacks").Select(c => new CategoryRecipe { Category = c }).ToList(),
                    Components = new List<RecipeComponent>
                    {
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Popcorn").First().IngredientId,
                            Size = 8,
                            Unit = RecipeComponent.UnitOfMeasure.Cup
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Butter").First().IngredientId,
                            Size = 1/4,
                            Unit = RecipeComponent.UnitOfMeasure.Cup
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Grated Cheese").First().IngredientId,
                            Size = 1/4,
                            Unit = RecipeComponent.UnitOfMeasure.Cup
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Pretzel Miniature").First().IngredientId,
                            Size = 2,
                            Unit = RecipeComponent.UnitOfMeasure.Cup
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Peanuts").First().IngredientId,
                            Size = 1,
                            Unit = RecipeComponent.UnitOfMeasure.Cup
                        },
                    },
                    Description = "Looking for tasty tidbits for the crowd to nibble on? Try this easy-to-make mix of popcorn, pretzels and nuts tossed with butter and Parmesan.",
                    Difficulty = 1,
                    Energy = 90,
                    Name = "Cheesy Parmesan Popcorn",
                    Picture = "cheesy-popcorn.jpg",
                    Procedure = new List<Step>
                    {
                        new Step
                        {

                            Index = 1,
                            Instruction = "Toss popcorn with butter and cheese in large bowl.",
                        },
                        new Step
                        {
                            Index = 2,
                            Instruction = "Add pretzels and nuts; mix lightly.",
                        },
          
                    },
                    Servings = 20,
                    Time = 10
                },
                new Recipe
                {
                    CategoriesRecipes = categories.Where(c => c.Name == "Appetizers & Snacks").Select(c => new CategoryRecipe { Category = c }).ToList(),
                    Components = new List<RecipeComponent>
                    {
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Saltine Cracker").First().IngredientId,
                            Size = 3,
                            Unit = RecipeComponent.UnitOfMeasure.Cup
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Peanuts").First().IngredientId,
                            Size = 2,
                            Unit = RecipeComponent.UnitOfMeasure.Cup
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Butter").First().IngredientId,
                            Size = 3,
                            Unit = RecipeComponent.UnitOfMeasure.Tablespoon
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Pepper").First().IngredientId,
                            Size = 1,
                            Unit = RecipeComponent.UnitOfMeasure.Teaspoon
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Cumin").First().IngredientId,
                            Size = 1/2,
                            Unit = RecipeComponent.UnitOfMeasure.Teaspoon
                        },
                        new RecipeComponent
                        {
                            IngredientId = ingredients.Where(i => i.Name == "Garlic").First().IngredientId,
                            Size = 1/4,
                            Unit = RecipeComponent.UnitOfMeasure.Teaspoon
                        },
                    },
                    Description = "Lots of great flavors here—for not a lot of trouble! This Chili Snack Mix is made with mini saltine crackers, mixed nuts, a bit of butter and spices.",
                    Difficulty = 2,
                    Energy = 150,
                    Name = "Chili Snack Mix",
                    Picture = "chili-snack-mix.jpg",
                    Procedure = new List<Step>
                    {
                        new Step
                        {

                            Index = 1,
                            Instruction = "Heat oven to 375ºF.",
                        },
                        new Step
                        {
                            Index = 2,
                            Instruction = "Combine crackers and nuts in large bowl. Mix butter and seasonings. Drizzle over cracker mixture; toss to coat.",
                        },
                        new Step
                        {
                            Index = 2,
                            Instruction = "Spread onto bottom of foil-lined 15x10x1-inch pan.",
                        },
                        new Step
                        {
                            Index = 2,
                            Instruction = "Bake 10 min. or until lightly toasted, stirring after 5 min. Cool.",
                        },
                    },
                    Servings = 20,
                    Time = 20
                }
            };

            context.Set<Recipe>().AddRange(recipes);
            await context.SaveChangesAsync();
        }
    }
}
