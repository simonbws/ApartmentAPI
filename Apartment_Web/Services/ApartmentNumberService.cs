using Apartment_Utility;
using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;

namespace Apartment_Web.Services
{
    public class ApartmentNumberService : BaseService, IApartmentNumberService // base service in order to be able to make calls to api
    {
        private readonly IHttpClientFactory _clientFactory;
        private string apartmentUrl;

        public ApartmentNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) //base service require IHTTPCLIENT FACTORY so we need to add that to the base class
        {
            _clientFactory = clientFactory;
            apartmentUrl = configuration.GetValue<string>("ServiceUrls:Apartment_API");

        }
        public Task<T> CreateAsync<T>(ApartmentNumberCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = apartmentUrl + "/api/apartmentNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = apartmentUrl + "/api/apartmentNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = apartmentUrl + "/api/apartmentNumberAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = apartmentUrl + "/api/apartmentNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ApartmentNumberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = apartmentUrl + "/api/apartmentNumberAPI/" + dto.ApartmentNo,
                Token = token
            });
        }
    }
}
