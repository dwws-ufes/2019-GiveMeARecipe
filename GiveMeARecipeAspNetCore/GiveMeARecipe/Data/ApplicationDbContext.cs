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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeComponent> RecipeComponents { get; set; }
        public DbSet<UserEvaluation> UserEvaluations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRecipe>()
                .HasKey(aur => new { aur.ApplicationUserId, aur.RecipeId });
            modelBuilder.Entity<ApplicationUserRecipe>()
                .HasOne(aur => aur.ApplicationUser)
                .WithMany(au => au.ApplicationUsersRecipes)
                .HasForeignKey(aur => aur.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserRecipe>()
                .HasOne(aur => aur.Recipe)
                .WithMany(r => r.ApplicationUsersRecipes)
                .HasForeignKey(aur => aur.RecipeId);

            modelBuilder.Entity<CategoryRecipe>()
                .HasKey(cr => new { cr.CategoryId, cr.RecipeId });
            modelBuilder.Entity<CategoryRecipe>()
                .HasOne(cr => cr.Category)
                .WithMany(c => c.CategoriesRecipes)
                .HasForeignKey(cr => cr.CategoryId);
            modelBuilder.Entity<CategoryRecipe>()
                .HasOne(cr => cr.Recipe)
                .WithMany(r => r.CategoriesRecipes)
                .HasForeignKey(cr => cr.RecipeId);
        }
    }
}
