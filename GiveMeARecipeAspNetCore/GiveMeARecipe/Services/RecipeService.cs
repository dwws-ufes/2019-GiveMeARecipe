using GiveMeARecipe.Data;
using GiveMeARecipe.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Services
{
    public class RecipeService : IService<Recipe>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Recipe> _dbSet;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;

            _dbSet = context.Set<Recipe>();
        }

        public Task<int> Create(Recipe item, ClaimsPrincipal user = null)
        {
            _dbSet.Add(item);

            return _context.SaveChangesAsync();
        }

        public Task Delete(long id, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Recipe> Get(long id, ClaimsPrincipal user = null)
        {
            var recipe = await _dbSet.FindAsync(id);

            return recipe;
        }

        public IQueryable<Recipe> GetAll(ClaimsPrincipal user = null)
        {
            var recipes = _dbSet;

            return recipes;
        }

        public Task<int> Update(Recipe item, ClaimsPrincipal user = null)
        {
            _dbSet.Update(item);

            return _context.SaveChangesAsync();
        }
    }
}
