using GiveMeARecipe.Data;
using GiveMeARecipe.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Services
{
    public class FavoritedRecipeService : IService<FavoritedRecipe>
    {
        private readonly DbSet<FavoritedRecipe> _dbSet;

        public FavoritedRecipeService(ApplicationDbContext context)
        {
            _dbSet = context.Set<FavoritedRecipe>();
        }

        public Task<int> Create(FavoritedRecipe item, ClaimsPrincipal user = null)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id, ClaimsPrincipal user = null)
        {
            throw new NotImplementedException();
        }

        public Task<FavoritedRecipe> Get(long id, ClaimsPrincipal user = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FavoritedRecipe> GetAll(ClaimsPrincipal user = null)
        {
            var favoritedRecipes = _dbSet;

            return favoritedRecipes;
        }

        public Task<int> Update(FavoritedRecipe item, ClaimsPrincipal user = null)
        {
            throw new NotImplementedException();
        }
    }
}
