using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Apartment_API.Controllers
{
    [Route("api/ApartmentAPI")]
    [ApiController]
    public class ApartmentAPIController : ControllerBase
    {
        private readonly IApartmentRepository _dbApartment;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public ApartmentAPIController(IApartmentRepository dbApartment, IMapper mapper)
        {
            _dbApartment = dbApartment;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetApartments()
        {
            try
            {
                IEnumerable<Apartment> apartmentList = await _dbApartment.GetAllAsync();
                _response.Result = _mapper.Map<List<ApartmentDTO>>(apartmentList);
                _response.StatusCode = HttpStatusCode.OK;
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
        [HttpGet("{id:int}", Name = "GetApartment")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApartments(int id)
        {
            try
            {


                if (id == 0)
                {
                    return BadRequest();
                }
                var apartment = await _dbApartment.GetAsync(u => u.Id == id);
                if (apartment == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<ApartmentDTO>(apartment);
                _response.StatusCode = HttpStatusCode.OK;
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
        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateApartment([FromBody] ApartmentCreateDTO createDTO)
        {
            try
            {


                //if(!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState); 
                //}
                if (await _dbApartment.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Apartment already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                //if (apartmentDTO.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}


                //Ale tutaj zamiast ręcznie wpisywać zakomentowane wartości poniżej, korzystamy z AutoMappera (funkcjonalność dodana póżniej)
                Apartment apartment = _mapper.Map<Apartment>(createDTO);

                // mapujemy dane poniewaz musimy dodac DTO do bazy danych. DTO nie jest encja, więc EF Core nie wie jak to zapisać w SQL. Apartament to warstwa bazy danych DbSet<Apartment>
                // ApartmentDTO to warstwa API. Musimy zmapowaćDTO na model bazy danych (Apartment).
                //Używamy DTO, bo encje (model bazy) nie zawsze powinien wychodzić na zewnątrz API. DTO to filtr bezpieczeństwa i wygody między API, a bazą danych.
                //Apartment model = new()
                //{
                //    Name = createDTO.Name,
                //    Details = createDTO.Details,
                //    ImageUrl = createDTO.ImageUrl,
                //    Occupancy = createDTO.Occupancy,
                //    Rate = createDTO.Rate,
                //    Sqft = createDTO.Sqft,
                //    Amenity = createDTO.Amenity
                //};
                await _dbApartment.CreateAsync(apartment);
                _response.Result = _mapper.Map<ApartmentDTO>(apartment);
                _response.StatusCode = HttpStatusCode.Created;
                // return model will be _response
                return CreatedAtRoute("GetApartment", new { id = apartment.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteApartment")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteApartment(int id)
        {
            try
            {


                if (id == 0)
                {
                    return BadRequest();
                }
                var apartment = await _dbApartment.GetAsync(u => u.Id == id);
                if (apartment == null)
                {
                    return NotFound();
                }
                await _dbApartment.RemoveAsync(apartment);
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
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateApartment(int id, [FromBody] ApartmentUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                //var villa = ApartmentStore.apartmentList.FirstOrDefault(u => u.Id == id);
                //villa.Name = villaDTO.Name;
                //villa.Sqft = villaDTO.Sqft;
                //villa.Occupancy = villaDTO.Occupancy;
                Apartment model = _mapper.Map<Apartment>(updateDTO);
                //Auto Mapper
                //Apartment model = new()
                //{
                //    Name = updateDTO.Name,
                //    Details = updateDTO.Details,
                //    ImageUrl = updateDTO.ImageUrl,
                //    Occupancy = updateDTO.Occupancy,
                //    Rate = updateDTO.Rate,
                //    Sqft = updateDTO.Sqft,
                //    Amenity = updateDTO.Amenity,
                //    CreatedDate = DateTime.Now
                //};
                await _dbApartment.UpdateAsync(model);
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
        [HttpPatch("{id:int}", Name = "UpdatePartialApartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialApartment(int id, JsonPatchDocument<ApartmentUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var apartment = await _dbApartment.GetAsync(u => u.Id == id, tracked: false);

            ApartmentUpdateDTO apartmentDTO = _mapper.Map<ApartmentUpdateDTO>(apartment);
            //ApartmentUpdateDTO apartmentDTO = new()
            //{
            //    Amenity = apartment.Amenity,
            //    Details = apartment.Details,
            //    Id = apartment.Id,
            //    ImageUrl = apartment.ImageUrl,
            //    Name = apartment.Name,
            //    Occupancy = apartment.Occupancy,
            //    Rate = apartment.Rate,
            //    Sqft = apartment.Sqft
            //};
            if (apartment == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(apartmentDTO, ModelState);
            //Auto Mapper, destination is Apartment and the source is apartmentDTO
            Apartment model = _mapper.Map<Apartment>(apartmentDTO);
            //Apartment model = new Apartment()
            //{
            //    Amenity = apartmentDTO.Amenity,
            //    Details = apartmentDTO.Details,
            //    Id = apartmentDTO.Id,
            //    ImageUrl = apartmentDTO.ImageUrl,
            //    Name = apartmentDTO.Name,
            //    Occupancy = apartmentDTO.Occupancy,
            //    Rate = apartmentDTO.Rate,
            //    Sqft = apartmentDTO.Sqft
            //};
            await _dbApartment.UpdateAsync(model);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

