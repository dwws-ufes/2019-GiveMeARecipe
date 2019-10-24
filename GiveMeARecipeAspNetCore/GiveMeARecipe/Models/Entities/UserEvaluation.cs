using GiveMeARecipe.Areas.Identity.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GiveMeARecipe.Models.Entities
{
    public class UserEvaluation
    {
        public long UserEvaluationId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        [Range(0, 5)]
        public int Value { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public long EvaluationId { get; set; }
        public virtual Evaluation Evaluation { get; set; }
    }
}
