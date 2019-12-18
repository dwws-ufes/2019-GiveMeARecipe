using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveMeARecipe.Models.Entities
{
    public class Evaluation
    {
        public long EvaluationId { get; set; }
        public string RecipeId { get; set; }
        public float Value { get; set; }

        public virtual IList<UserEvaluation> Evaluations { get; set; }
    }
}
