using GiveMeARecipe.Areas.Identity.Models;

namespace GiveMeARecipe.Models.Entities
{
    public class ApplicationUserRecipe
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
