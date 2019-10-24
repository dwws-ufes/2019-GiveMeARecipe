using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiveMeARecipe.Models.ViewModels
{
    public class IndexViewModel
    {
        public int? Difficulty { get; set; }
        public int? Rating { get; set; }
        public IEnumerable<long> CategoriesIds { get; set; }
        [Required]
        public IEnumerable<long> IngredientsIds { get; set; }
    }
}
