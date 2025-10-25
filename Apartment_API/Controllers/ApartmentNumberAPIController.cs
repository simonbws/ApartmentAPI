using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Apartment_API.Controllers
{
    [Route("api/ApartmentNumberAPI")]
    [ApiController]
    public class ApartmentNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IApartmentNumberRepository _dbApartmentNumber;
        private readonly IMapper _mapper;

        public ApartmentNumberAPIController(IApartmentNumberRepository dbApartmentNumber, IMapper mapper)
        {
            _dbApartmentNumber = dbApartmentNumber;
            _mapper = mapper;
            this._response = new APIResponse();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetApartmentNumbers()
        {
            try
            {


                IEnumerable<ApartmentNumber> apartmentNumberList = await _dbApartmentNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<ApartmentNumberDTO>>(apartmentNumberList);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetApartmentNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApartmentNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }
                var apartmentNumber = await _dbApartmentNumber.GetAsync(u => u.ApartmentNo == id);
                if (apartmentNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return _response;
                }
                _response.Result = _mapper.Map<ApartmentNumberDTO>(apartmentNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateApartmentNumber([FromBody] ApartmentNumberCreateDTO createDTO)
        {
            try
            {
                if (await _dbApartmentNumber.GetAsync(u => u.ApartmentNo == createDTO.ApartmentNo) != null)
                {
                    ModelState.AddModelError("CustomError", "Apartment Number already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                ApartmentNumber apartmentNumber = _mapper.Map<ApartmentNumber>(createDTO);

                await _dbApartmentNumber.CreateAsync(apartmentNumber);
                _response.Result = _mapper.Map<ApartmentNumberDTO>(apartmentNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetApartment", new { id = apartmentNumber.ApartmentNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteApartmentNumber")]
        public async Task<ActionResult<APIResponse>> DeleteApartmentNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var apartmentNumber = await _dbApartmentNumber.GetAsync(u => u.ApartmentNo == id);
                if (apartmentNumber == null)
                {
                    return NotFound();
                }
                await _dbApartmentNumber.RemoveAsync(apartmentNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPut("{id:int}", Name = "UpdateApartmentNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateApartmentNumber(int id, [FromBody] ApartmentNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.ApartmentNo)
                {
                    return BadRequest();
                }

                ApartmentNumber model = _mapper.Map<ApartmentNumber>(updateDTO);

                await _dbApartmentNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }

}

