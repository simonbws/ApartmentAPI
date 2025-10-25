using Apartment_Utility;
using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;

namespace Apartment_Web.Services
{
    public class ApartmentService : BaseService, IApartmentService // base service in order to be able to make calls to api
    {
        private readonly IHttpClientFactory _clientFactory;
        private string apartmentUrl;

        public ApartmentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) //base service require IHTTPCLIENT FACTORY so we need to add that to the base class
        {
            _clientFactory = clientFactory;
            apartmentUrl = configuration.GetValue<string>("ServiceUrls:Apartment_API");

        }
        public Task<T> CreateAsync<T>(ApartmentCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = apartmentUrl + "/api/apartmentAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = apartmentUrl + "/api/apartmentAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = apartmentUrl + "/api/apartmentAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = apartmentUrl + "/api/apartmentAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(ApartmentUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = apartmentUrl + "/api/apartmentAPI/" + dto.Id
            });
        }
    }
}
