using GiveMeARecipe.Areas.Identity.Models;

namespace GiveMeARecipe.Models.Entities
{
    public class FavoritedRecipe
    {
        public long FavoritedRecipeId { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string RecipeId { get; set; }
    }
}
