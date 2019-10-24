namespace GiveMeARecipe.Models.Entities
{
    public class Step
    {
        public long StepId { get; set; }
        public int Index { get; set; }
        public string Instruction { get; set; }

        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
