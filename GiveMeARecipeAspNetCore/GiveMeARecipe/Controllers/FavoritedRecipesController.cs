using GiveMeARecipe.Areas.Identity.Models;
using GiveMeARecipe.Models.Entities;
using GiveMeARecipe.Models.ViewModels;
using GiveMeARecipe.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Controllers
{
    public class FavoritedRecipesController : Controller
    {
        private readonly IService<Recipe> _recipeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoritedRecipesController(IService<Recipe> recipeService, UserManager<ApplicationUser> userManager)
        {
            _recipeService = recipeService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipes = _recipeService.GetAll().Where(r => r.ApplicationUsersRecipes.Any(aur => aur.ApplicationUserId == nameIdentifier));

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

            return View(recipesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FavoritedRecipeViewModel model)
        {
            var recipe = await _recipeService.Get(model.RecipeId);

            if (!User.Identity.IsAuthenticated)
            {
                RedirectToPage("LoginPage");
            }

            var user = await _userManager.GetUserAsync(User);

            if (user.ApplicationUsersRecipes.All(aur => aur.RecipeId != model.RecipeId))
            {
                recipe.ApplicationUsersRecipes.Add(new ApplicationUserRecipe
                {
                    ApplicationUser = user
                });

                await _recipeService.Update(recipe);
            }

            return RedirectToAction("Show", "Recipes", new { id = model.RecipeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(FavoritedRecipeViewModel model)
        {
            var recipe = await _recipeService.Get(model.RecipeId);

            if (!User.Identity.IsAuthenticated)
            {
                RedirectToPage("LoginPage");
            }

            var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationUserRecipe = recipe.ApplicationUsersRecipes.FirstOrDefault(aur => aur.RecipeId == model.RecipeId && aur.ApplicationUserId == nameIdentifier);

            if (applicationUserRecipe != null)
            {
                recipe.ApplicationUsersRecipes.Remove(applicationUserRecipe);

                await _recipeService.Update(recipe);
            }

            return RedirectToAction("Show", "Recipes", new { id = model.RecipeId });
        }
    }
}
