using System;
using System.ComponentModel.DataAnnotations;

namespace GiveMeARecipe.Models.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        public string Comment { get; set; }
        public bool CurrentUserReview { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int Evaluation { get; set; }
        public long RecipeId { get; set; }
        public string UserName { get; set; }
    }
}
