using Apartment_API.Models;
using System.Linq.Expressions;

namespace Apartment_API.Repository.IRepository
{
    public interface IApartmentRepository
    {
        Task<List<Apartment>> GetAllAsync(Expression<Func<Apartment, bool>> filter = null);
        Task<Apartment> GetAsync(Expression<Func<Apartment, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Apartment entity);
        Task UpdateAsync(Apartment entity);
        Task RemoveAsync(Apartment entity);
        Task SaveAsync();
    }
}
