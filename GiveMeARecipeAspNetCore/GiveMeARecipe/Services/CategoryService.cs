using GiveMeARecipe.Data;
using GiveMeARecipe.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly DbSet<Category> _dbSet;

        public CategoryService(ApplicationDbContext context)
        {
            _dbSet = context.Set<Category>();
        }

        public Task<int> Create(Category item, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long id, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> Get(long id, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Category> GetAll(ClaimsPrincipal user = null)
        {
            var categories = _dbSet;

            return categories;
        }

        public Task<int> Update(Category item, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
