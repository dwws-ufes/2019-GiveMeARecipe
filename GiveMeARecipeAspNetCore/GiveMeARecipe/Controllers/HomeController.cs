using GiveMeARecipe.Models.Entities;
using GiveMeARecipe.Models.ViewModels;
using GiveMeARecipe.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GiveMeARecipe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Category> _categoryService;
        private readonly IService<Ingredient> _ingredientService;
        private readonly IService<Recipe> _recipeService;

        public HomeController(IService<Category> categoryService, IService<Ingredient> ingredientService, IService<Recipe> recipeService)
        {
            _categoryService = categoryService;
            _ingredientService = ingredientService;
            _recipeService = recipeService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _categoryService.GetAll().OrderBy(c => c.Name).Select(i => new SelectListItem { Text = i.Name, Value = i.CategoryId.ToString() }).ToListAsync();
            ViewBag.Ingredients = await _ingredientService.GetAll().OrderBy(i => i.Name).Select(i => new SelectListItem { Text = i.Name, Value = i.IngredientId.ToString() }).ToListAsync();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
