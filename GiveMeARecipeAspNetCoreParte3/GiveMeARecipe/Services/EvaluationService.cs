using GiveMeARecipe.Data;
using GiveMeARecipe.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Services
{
    public class EvaluationService : IService<Evaluation>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Evaluation> _dbSet;

        public EvaluationService(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Evaluation>();
        }

        public Task<int> Create(Evaluation item, ClaimsPrincipal user = null)
        {
            _dbSet.Add(item);

            return _context.SaveChangesAsync();
        }

        public Task Delete(long id, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<Evaluation> Get(long id, ClaimsPrincipal user = null)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Evaluation> GetAll(ClaimsPrincipal user = null)
        {
            var evaluations = _dbSet;

            return evaluations;
        }

        public Task<int> Update(Evaluation item, ClaimsPrincipal user = null)
        {
            _dbSet.Update(item);

            return _context.SaveChangesAsync();
        }
    }
}
