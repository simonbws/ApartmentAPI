using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Apartment_API.Controllers
{
    [Route("api/v{version:apiVersion}/ApartmentNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    
    public class ApartmentNumberV2APIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IApartmentNumberRepository _dbApartmentNumber;
        private readonly IApartmentRepository _dbApartment;
        private readonly IMapper _mapper;

        public ApartmentNumberV2APIController(IApartmentNumberRepository dbApartmentNumber, IMapper mapper, IApartmentRepository dbApartment)
        {
            _dbApartmentNumber = dbApartmentNumber;
            _mapper = mapper;
            this._response = new APIResponse();
            _dbApartment = dbApartment;
        }    
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }

}

