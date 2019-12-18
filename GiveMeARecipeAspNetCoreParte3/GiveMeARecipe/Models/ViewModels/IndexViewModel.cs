using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiveMeARecipe.Models.ViewModels
{
    public class IndexViewModel
    {
        public int? Rating { get; set; }
        public IEnumerable<string> CategoriesUris { get; set; }
        [Required]
        public IEnumerable<string> IngredientsUris { get; set; }
    }
}
