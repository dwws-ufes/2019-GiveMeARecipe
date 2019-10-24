namespace GiveMeARecipe.Models.Entities
{
    public class RecipeComponent
    {
        public long RecipeComponentId { get; set; }
        public string Condition { get; set; }
        public bool Optional { get; set; }
        public float Size { get; set; }

        public enum UnitOfMeasure
        {
            Can,
            Centimeter,
            Cup,
            Deciliter,
            FluidOunce,
            Gallon,
            Gill,
            Gram,
            Inch,
            Kilogram,
            Liter,
            Meter,
            Milliliter,
            Millimeter,
            Milligram,
            Ounce,
            Pint,
            Piece,
            Pound,
            Quart,
            Tablespoon,
            Teaspoon,
            Unit
        }
        public UnitOfMeasure Unit { get; set; }

        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
