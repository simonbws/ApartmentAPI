using Apartment_API.Models;

namespace Apartment_API.Repository.IRepository
{
    public interface IApartmentNumberRepository : IRepository<ApartmentNumber>
    {
        Task<ApartmentNumber> UpdateAsync(ApartmentNumber entity);
    }
}
