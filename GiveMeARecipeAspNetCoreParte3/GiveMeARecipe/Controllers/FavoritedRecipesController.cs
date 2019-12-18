using GiveMeARecipe.Areas.Identity.Models;
using GiveMeARecipe.Models.Entities;
using GiveMeARecipe.Models.ViewModels;
using GiveMeARecipe.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace GiveMeARecipe.Controllers
{
    public class FavoritedRecipesController : Controller
    {
        private readonly IService<FavoritedRecipe> _favoritedRecipeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoritedRecipesController(IService<FavoritedRecipe> favoritedRecipeService, UserManager<ApplicationUser> userManager)
        {
            _favoritedRecipeService = favoritedRecipeService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipesId = await _favoritedRecipeService.GetAll().Where(fr => fr.ApplicationUserId == nameIdentifier).Select(fr => fr.RecipeId).ToListAsync();
            var endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
            var favoritedRecipes = new List<RecipeViewModel>();

            foreach (var recipeId in recipesId)
            {
                var query =
                    "PREFIX dbo: <http://dbpedia.org/ontology/>\r\n" +
                    "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\r\n" +
                    "SELECT ?food ?stripped_name ?stripped_abstract ?thumb\r\n" +
                    "WHERE{\r\n" +
                        "?food a dbo:Food.\r\n" +
                        "?food rdfs:label ?name.\r\n" +
                        "?food dbo:abstract ?abstract.\r\n" +
                        "?food dbo:thumbnail ?thumb.\r\n" +
                        $"?food dbo:wikiPageID {recipeId}\r\n" +
                        "FILTER(LANGMATCHES(LANG(?name), 'en') && LANGMATCHES(LANG(?abstract), 'en'))\r\n" +
                        "BIND(STR(?name)  AS ?stripped_name)\r\n" +
                        "BIND(STR(?abstract) AS ?stripped_abstract)\r\n" +
                    "}";

                var results = endpoint.QueryWithResultSet(query);

                if (results.Any())
                {
                    favoritedRecipes.Add(new RecipeViewModel
                    {
                        Description = results.First()["stripped_abstract"].ToString(),
                        Evaluation = 0, //recipe.Evaluation != null ? recipe.Evaluation.Value : 0,
                        FavoritedRecipe = false, // recipe.ApplicationUsersRecipes.Any(aur => aur.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)),
                        Name = results.First()["stripped_name"].ToString(),
                        Picture = results.First()["thumb"].ToString(),
                        RecipeId = recipeId
                    });
                }
            }

            var recipesViewModel = new RecipesViewModel
            {
                Recipes = favoritedRecipes
            };

            return View(recipesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FavoritedRecipeViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToPage("LoginPage");
            }

            var user = await _userManager.GetUserAsync(User);

            if (user.FavoritedRecipes.All(fr => fr.RecipeId != model.RecipeId))
            {
                var favoritedRecipe = new FavoritedRecipe
                {
                    RecipeId = model.RecipeId
                };

                user.FavoritedRecipes.Add(favoritedRecipe);

                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Show", "Recipes", new { id = model.RecipeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(FavoritedRecipeViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToPage("LoginPage");
            }

            var user = await _userManager.GetUserAsync(User);

            var favoritedRecipe = user.FavoritedRecipes.FirstOrDefault(fr => fr.RecipeId == model.RecipeId);
            
            if (favoritedRecipe != null)
            {
                user.FavoritedRecipes.Remove(favoritedRecipe);

                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Show", "Recipes", new { id = model.RecipeId });
        }
    }
}
