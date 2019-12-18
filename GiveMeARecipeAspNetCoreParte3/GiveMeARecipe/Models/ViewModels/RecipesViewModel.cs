using System.Collections.Generic;

namespace GiveMeARecipe.Models.ViewModels
{
    public class RecipesViewModel
    {
        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}
