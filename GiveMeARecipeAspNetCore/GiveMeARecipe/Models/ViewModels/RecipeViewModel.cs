using System.Collections.Generic;

namespace GiveMeARecipe.Models.ViewModels
{
    public class RecipeViewModel
    {
        public long RecipeId { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public float Energy { get; set; }
        public float Evaluation { get; set; }
        public bool FavoritedRecipe { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Servings { get; set; }
        public int Time { get; set; }
        public bool UserHasReview { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
        public IEnumerable<string> Steps { get; set; }
    }
}
