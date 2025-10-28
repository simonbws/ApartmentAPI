using Apartment_Web.Models.DTO;

namespace Apartment_Web.Services.IServices
{
    public interface IApartmentNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ApartmentNumberCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ApartmentNumberUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
