using GiveMeARecipe.Data;
using GiveMeARecipe.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Services
{
    public class IngredientService : IService<Ingredient>
    {
        private readonly DbSet<Ingredient> _dbSet;

        public IngredientService(ApplicationDbContext context)
        {
            _dbSet = context.Set<Ingredient>();
        }

        public Task<int> Create(Ingredient item, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long id, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<Ingredient> Get(long id, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Ingredient> GetAll(ClaimsPrincipal user = null)
        {
            var ingredients = _dbSet;

            return ingredients;
        }

        public Task<int> Update(Ingredient item, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
