using GiveMeARecipe.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GiveMeARecipe.Areas.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual IList<UserEvaluation> UserEvaluations { get; set; }
        public virtual IList<FavoritedRecipe> FavoritedRecipes { get; set; }
    }
}
