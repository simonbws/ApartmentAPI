using Apartment_API.Models;
using System.Linq.Expressions;

namespace Apartment_API.Repository.IRepository
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        Task<Apartment> UpdateAsync(Apartment entity);

    }
}
