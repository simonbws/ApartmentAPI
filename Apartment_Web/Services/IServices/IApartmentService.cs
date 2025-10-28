using Apartment_Web.Models.DTO;

namespace Apartment_Web.Services.IServices
{
    public interface IApartmentService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ApartmentCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ApartmentUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
