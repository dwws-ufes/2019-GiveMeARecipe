using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiveMeARecipe.Services
{
    public interface IService<T> where T : class
    {
        Task<int> Create(T item, ClaimsPrincipal user = null);
        Task Delete(long id, ClaimsPrincipal user = null);
        Task<T> Get(long id, ClaimsPrincipal user = null);
        IQueryable<T> GetAll(ClaimsPrincipal user = null);
        Task<int> Update(T item, ClaimsPrincipal user = null);
    }
}
