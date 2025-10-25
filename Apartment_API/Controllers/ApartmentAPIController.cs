using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apartment_API.Controllers
{
    [Route("api/ApartmentAPI")]
    [ApiController]
    public class ApartmentAPIController : ControllerBase
    {
        private readonly IApartmentRepository _dbApartment;
        private readonly IMapper _mapper;
        public ApartmentAPIController(IApartmentRepository dbApartment, IMapper mapper)
        {
            _dbApartment = dbApartment;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApartmentDTO>> GetApartments()
        {
            IEnumerable<Apartment> apartmentList = await _dbApartment.GetAllAsync();
            return Ok(_mapper.Map<List<ApartmentDTO>>(apartmentList));
        }

        [HttpGet("{id:int}", Name = "GetApartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApartmentDTO>> GetApartments(int id)
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
            return Ok(_mapper.Map<ApartmentDTO>(apartment));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApartmentDTO>> CreateApartment([FromBody] ApartmentCreateDTO createDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState); 
            //}
            if (await _dbApartment.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Apartment already Exists!");
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
            Apartment model = _mapper.Map<Apartment>(createDTO);

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
            await _dbApartment.CreateAsync(model);
            return CreatedAtRoute("GetApartment", new { id = model.Id }, model);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteApartment")]
        public async Task<ActionResult<ApartmentDTO>> DeleteApartment(int id)
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
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody] ApartmentUpdateDTO updateDTO)
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
            _dbApartment.UpdateAsync(model);
            return NoContent();
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
            var apartment = await _dbApartment.GetAsync(u => u.Id == id, tracked:false);

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

