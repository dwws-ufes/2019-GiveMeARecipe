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

namespace GiveMeARecipe.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IService<Evaluation> _evaluationService;

        public ReviewsController(IService<Evaluation> evaluationService)
        {
            _evaluationService = evaluationService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, ReviewViewModel model)
        {
            var evaluation = await _evaluationService.GetAll().FirstOrDefaultAsync(e => e.RecipeId == id);
            var update = true;

            if (evaluation == null)
            {
                evaluation = new Evaluation
                {
                    Evaluations = new List<UserEvaluation>(),
                    RecipeId = id
                };

                update = false;
            }

            evaluation.Value = (evaluation.Value * evaluation.Evaluations.Count + model.Evaluation) / (evaluation.Evaluations.Count + 1);
            evaluation.Evaluations.Add(new UserEvaluation
            {
                ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Comment = model.Comment,
                Date = DateTime.Now,
                Value = model.Evaluation
            });

            if (update)
            {
                await _evaluationService.Update(evaluation);
            }
            else
            {
                await _evaluationService.Create(evaluation);
            }

            TempData["success_message"] = "Your review for this recipe was successfully posted.";

            return RedirectToAction("Show", "Recipes", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var evaluation = await _evaluationService.GetAll().FirstOrDefaultAsync(e => e.RecipeId == id);

            if (evaluation == null)
            {
                return RedirectToAction("Show", "Recipes", new { id });
            }

            var userEvaluation = evaluation.Evaluations.FirstOrDefault(e => e.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (userEvaluation == null)
            {
                return RedirectToAction("Show", "Recipes", new { id });
            }

            if (evaluation.Evaluations.Count - 1 == 0)
            {
                evaluation.Value = 0;
            }
            else
            {
                evaluation.Value = (evaluation.Value * evaluation.Evaluations.Count - userEvaluation.Value) / (evaluation.Evaluations.Count - 1);
            }

            evaluation.Evaluations.Remove(userEvaluation);

            await _evaluationService.Update(evaluation);

            TempData["success_message"] = "Your review for this recipe was successfully deleted.";

            return RedirectToAction("Show", "Recipes", new { id });
        }
    }
}
