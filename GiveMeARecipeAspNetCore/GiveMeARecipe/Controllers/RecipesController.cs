using GiveMeARecipe.Models.Entities;
using GiveMeARecipe.Models.ViewModels;
using GiveMeARecipe.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IService<Recipe> _recipeService;

        public RecipesController(IService<Recipe> recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<IActionResult> Show(long id)
        {
            var recipe = await _recipeService.Get(id);
            var recipeViewModel = new RecipeViewModel
            {
                Categories = recipe.CategoriesRecipes.Select(cr => cr.Category.Name).ToList(),
                Description = recipe.Description,
                Difficulty = recipe.Difficulty,
                Energy = recipe.Energy,
                Evaluation = recipe.Evaluation != null ? recipe.Evaluation.Value : 0,
                FavoritedRecipe = recipe.ApplicationUsersRecipes.Any(aur => aur.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Ingredients = recipe.Components.Select(c => FormatIngredient(c)).ToList(),
                Name = recipe.Name,
                Picture = recipe.Picture,
                RecipeId = recipe.RecipeId,
                Reviews = recipe.Evaluation?.Evaluations.Select(e => new ReviewViewModel
                {
                    Comment = e.Comment,
                    CurrentUserReview = User.Identity.IsAuthenticated ? e.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier) : false,
                    Date = e.Date,
                    Evaluation = e.Value,
                    UserName = e.ApplicationUser.UserName.Split('@')[0],
                }),
                Servings = recipe.Servings,
                Steps = recipe.Procedure.OrderBy(p => p.Index).Select(p => p.Instruction).ToList(),
                Time = recipe.Time,
                UserHasReview = User.Identity.IsAuthenticated && recipe.Evaluation != null ?
                    recipe.Evaluation.Evaluations.Any(e => e.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)) :
                    false
            };

            return View(recipeViewModel);
        }

        public async Task<IActionResult> ShowAll(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_NoIngredientsPartial");
            }

            var recipes = _recipeService.GetAll().Where(r => r.Components.All(c => model.IngredientsIds.Contains(c.IngredientId)));

            if (model.CategoriesIds != null)
            {
                recipes = recipes.Where(r => r.CategoriesRecipes.Any(cr => model.CategoriesIds.Contains(cr.CategoryId)));
            }

            if (model.Difficulty.HasValue)
            {
                recipes = recipes.Where(r => r.Difficulty >= model.Difficulty.Value);
            }

            if (model.Rating.HasValue)
            {
                recipes = recipes.Where(r => r.Evaluation != null && r.Evaluation.Value >= model.Rating.Value);
            }

            var recipesViewModel = new RecipesViewModel
            {
                Recipes = await recipes.Select(r => new RecipeViewModel
                {
                    Description = r.Description,
                    Difficulty = r.Difficulty,
                    Evaluation = r.Evaluation.Value,
                    Ingredients = r.Components.Select(c => c.Ingredient.Name),
                    Name = r.Name,
                    Picture = r.Picture,
                    RecipeId = r.RecipeId,
                    Servings = r.Servings,
                    Time = r.Time
                }).ToListAsync()
            };

            return PartialView("_RecipesPartial", recipesViewModel);
        }

        private string FormatIngredient(RecipeComponent component)
        {
            var ingredient = $"{component.Ingredient.Name}, {component.Size} {component.Unit}";

            if (component.Condition != null)
            {
                ingredient += $", {component.Condition}";
            }

            if (component.Optional)
            {
                ingredient += $" (optional)";
            }

            return ingredient;
        }
    }
}
