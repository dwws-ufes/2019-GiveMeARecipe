using System.ComponentModel.DataAnnotations;

namespace GiveMeARecipe.Models.ViewModels
{
    public class FavoritedRecipeViewModel
    {
        [Required]
        public string RecipeId { get; set; }
        public bool FavoritedRecipe { get; set; }
    }
}
