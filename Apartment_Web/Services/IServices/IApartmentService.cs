using Apartment_Web.Models.DTO;

namespace Apartment_Web.Services.IServices
{
    public interface IApartmentService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ApartmentCreateDTO dto);
        Task<T> UpdateAsync<T>(ApartmentUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
