using GiveMeARecipe.Models;
using GiveMeARecipe.Models.Entities;
using GiveMeARecipe.Models.ViewModels;
using GiveMeARecipe.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IService<Recipe> _recipeService;

        public ReviewsController(IService<Recipe> recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long id, ReviewViewModel model)
        {
            var recipe = await _recipeService.Get(id);

            if (recipe.Evaluation == null)
            {
                recipe.Evaluation = new Evaluation
                {
                    Evaluations = new List<UserEvaluation>()
                };
            }

            recipe.Evaluation.Value = (recipe.Evaluation.Value * recipe.Evaluation.Evaluations.Count + model.Evaluation) / (recipe.Evaluation.Evaluations.Count + 1);
            recipe.Evaluation.Evaluations.Add(new UserEvaluation
            {
                ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Comment = model.Comment,
                Date = DateTime.Now,
                Value = model.Evaluation
            });

            await _recipeService.Update(recipe);

            TempData["success_message"] = "Your review for this recipe was successfully posted.";

            return RedirectToAction("Show", "Recipes", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var recipe = await _recipeService.Get(id);

            if (recipe.Evaluation == null)
            {
                return RedirectToAction("Show", "Recipes", new { id });
            }

            var userEvaluation = recipe.Evaluation.Evaluations.FirstOrDefault(e => e.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (userEvaluation == null)
            {
                return RedirectToAction("Show", "Recipes", new { id });
            }

            if (recipe.Evaluation.Evaluations.Count - 1 == 0)
            {
                recipe.Evaluation.Value = 0;
            }
            else
            {
                recipe.Evaluation.Value = (recipe.Evaluation.Value * recipe.Evaluation.Evaluations.Count - userEvaluation.Value) / (recipe.Evaluation.Evaluations.Count - 1);
            }
            
            recipe.Evaluation.Evaluations.Remove(userEvaluation);

            await _recipeService.Update(recipe);

            TempData["success_message"] = "Your review for this recipe was successfully deleted.";

            return RedirectToAction("Show", "Recipes", new { id });
        }
    }
}
