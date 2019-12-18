using GiveMeARecipe.Areas.Identity.Models;
using GiveMeARecipe.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiveMeARecipe.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<FavoritedRecipe> FavoritedsRecipes { get; set; }
        public DbSet<UserEvaluation> UserEvaluations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
