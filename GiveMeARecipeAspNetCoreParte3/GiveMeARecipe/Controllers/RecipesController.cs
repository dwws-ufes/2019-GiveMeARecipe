using GiveMeARecipe.Models.Entities;
using GiveMeARecipe.Models.ViewModels;
using GiveMeARecipe.Services;
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
    public class RecipesController : Controller
    {
        private readonly IService<Evaluation> _evaluationService;
        private readonly IService<FavoritedRecipe> _favoritedRecipeService;
        public RecipesController(IService<Evaluation> evaluationService, IService<FavoritedRecipe> favoritedRecipeService)
        {
            _evaluationService = evaluationService;
            _favoritedRecipeService = favoritedRecipeService;
        }

        public async Task<IActionResult> Show(string id)
        {
            var endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
            var query =
                "PREFIX dbo: <http://dbpedia.org/ontology/>\r\n" +
                "PREFIX dct: <http://purl.org/dc/terms/>\r\n" +
                "PREFIX dbc: <http://dbpedia.org/resource/Category:>\r\n" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\r\n" +
                "SELECT ?food ?stripped_name ?stripped_abstract ?thumb ?ings ?cats\r\n" +
                "WHERE{\r\n" +
                    "?food a dbo:Food.\r\n" +
                    "?food rdfs:label ?name.\r\n" +
                    "?food dbo:abstract ?abstract.\r\n" +
                    "?food dbo:thumbnail ?thumb.\r\n" +
                    "?food dbo:ingredient ?ings.\r\n" +
                    "?food dct:subject ?cats.\r\n" +
                    $"?food dbo:wikiPageID {id}\r\n" +
                    "FILTER(LANGMATCHES(LANG(?name), 'en') && LANGMATCHES(LANG(?abstract), 'en'))\r\n" +
                    "BIND(STR(?name)  AS ?stripped_name)\r\n" +
                    "BIND(STR(?abstract) AS ?stripped_abstract)\r\n" +
                "}";
            var results = endpoint.QueryWithResultSet(query);

            if (!results.Any())
            {
                return NotFound();
            }

            var evaluation = await _evaluationService.GetAll().FirstOrDefaultAsync(e => e.RecipeId == id);
            var isFavorited = await _favoritedRecipeService.GetAll().AnyAsync(fr => fr.RecipeId == id && fr.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var recipeViewModel = new RecipeViewModel
            {
                Categories = results.Select(r => r["cats"].ToString()).Distinct().ToList(),
                Description = results.First()["stripped_abstract"].ToString(),
                Evaluation = evaluation != null ? evaluation.Value : 0,
                FavoritedRecipe = isFavorited,
                Ingredients = results.Select(r => r["ings"].ToString()).Distinct().ToList(),
                Name = results.First()["stripped_name"].ToString(),
                Picture = results.First()["thumb"].ToString(),
                RecipeId = id,
                Reviews = evaluation?.Evaluations.Select(e => new ReviewViewModel
                {
                    Comment = e.Comment,
                    CurrentUserReview = User.Identity.IsAuthenticated ? e.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier) : false,
                    Date = e.Date,
                    Evaluation = e.Value,
                    UserName = e.ApplicationUser.UserName.Split('@')[0],
                }),
                UserHasReview = User.Identity.IsAuthenticated && evaluation != null ?
                    evaluation.Evaluations.Any(e => e.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)) :
                    false
            };

            return View(recipeViewModel);
        }

        public IActionResult ShowAll(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_NoIngredientsPartial");
            }

            var ingredients = model.IngredientsUris.Select(i => $"exists {{ ?food dbo:ingredient dbr:{i.Split('/').Last()} }}" );
            var ingredients_exists = $"FILTER ({string.Join(" && ", ingredients)})\r\n";
            var categories = model.CategoriesUris?.Select(c => $"exists {{ ?food dct:subject dbc:{c.Split(':').Last()} }}");
            var categories_exists = categories != null ? $"FILTER ({string.Join(" && ", categories)})\r\n" : "";

            var endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
            var query =
                "PREFIX dbo: <http://dbpedia.org/ontology/>\r\n" +
                "PREFIX dbr: <http://dbpedia.org/resource/>\r\n" +
                "PREFIX dct: <http://purl.org/dc/terms/>\r\n" +
                "PREFIX dbc: <http://dbpedia.org/resource/Category:>\r\n" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\r\n" +
                "SELECT ?food ?stripped_name ?stripped_abstract ?thumb ?id\r\n" +
                "WHERE{\r\n" +
                    "?food a dbo:Food.\r\n" +
                    "?food rdfs:label ?name.\r\n" +
                    "?food dbo:abstract ?abstract.\r\n" +
                    "?food dbo:thumbnail ?thumb.\r\n" +
                    "?food dbo:wikiPageID ?id\r\n" +
                    ingredients_exists +
                    categories_exists +
                    "FILTER(LANGMATCHES(LANG(?name), 'en') && LANGMATCHES(LANG(?abstract), 'en'))\r\n" +
                    "BIND(STR(?name)  AS ?stripped_name)\r\n" +
                    "BIND(STR(?abstract) AS ?stripped_abstract)\r\n" +
                "}";
            var results = endpoint.QueryWithResultSet(query);

            var recipesViewModel = new RecipesViewModel
            {
                Recipes = results.Select(r => new RecipeViewModel
                {
                    Description = r["stripped_abstract"].ToString(),
                    Name = r["stripped_name"].ToString(),
                    Picture = r["thumb"].ToString(),
                    RecipeId = r["id"].ToString().Split("^^").First(),
                    Evaluation = GetEvaluation(r["id"].ToString().Split("^^").First())
                }).ToList()
            };

            if (model.Rating.HasValue)
            {
                recipesViewModel.Recipes = recipesViewModel.Recipes.Where(r => r.Evaluation >= model.Rating.Value).ToList();
            }

            return PartialView("_RecipesPartial", recipesViewModel);
        }

        private float GetEvaluation(string recipeId)
        {
            var evaluation = _evaluationService.GetAll().FirstOrDefault(e => e.RecipeId == recipeId);

            return evaluation != null ? evaluation.Value : 0;
        }
    }
}
