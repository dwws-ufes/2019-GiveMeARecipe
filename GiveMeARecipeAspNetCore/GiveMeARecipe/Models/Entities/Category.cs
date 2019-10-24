using System.Collections.Generic;

namespace GiveMeARecipe.Models.Entities
{
    public class Category
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }

        public virtual IList<CategoryRecipe> CategoriesRecipes { get; set; }
    }
}
