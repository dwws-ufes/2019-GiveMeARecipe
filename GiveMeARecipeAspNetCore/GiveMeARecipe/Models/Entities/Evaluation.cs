using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveMeARecipe.Models.Entities
{
    public class Evaluation
    {
        [ForeignKey("Recipe")]
        public long EvaluationId { get; set; }
        public float Value { get; set; }

        public virtual Recipe Recipe { get; set; }

        public virtual IList<UserEvaluation> Evaluations { get; set; }
    }
}
