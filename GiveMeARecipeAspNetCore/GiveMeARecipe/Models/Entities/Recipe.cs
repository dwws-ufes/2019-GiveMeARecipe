using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiveMeARecipe.Models.Entities
{
    public class Recipe
    {
        public long RecipeId { get; set; }
        public string Description { get; set; }
        [Range(1, 3)]
        public int Difficulty { get; set; }
        public float Energy { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Servings { get; set; }
        public int Time { get; set; }

        public virtual IList<CategoryRecipe> CategoriesRecipes { get; set; }
        public virtual Evaluation Evaluation { get; set; }

        public virtual IList<ApplicationUserRecipe> ApplicationUsersRecipes { get; set; }
        public virtual IList<Step> Procedure { get; set; }
        public virtual IList<RecipeComponent> Components { get; set; }
    }
}
