namespace GiveMeARecipe.Models.Entities
{
    public class CategoryRecipe
    {
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
