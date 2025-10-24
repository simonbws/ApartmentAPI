using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apartment_API.Controllers
{
    [Route("api/ApartmentAPI")]
    [ApiController]
    public class ApartmentAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ApartmentAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApartmentDTO>> GetApartments()
        {
            return Ok(await _db.Apartments.ToListAsync());
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
            var apartment = await _db.Apartments.FirstOrDefaultAsync(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return Ok(apartment);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApartmentDTO>> CreateApartment([FromBody] ApartmentCreateDTO apartmentDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState); 
            //}
            if (await _db.Apartments.FirstOrDefaultAsync(u => u.Name.ToLower() == apartmentDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Apartment already Exists!");
                return BadRequest(ModelState);
            }
            if (apartmentDTO == null)
            {
                return BadRequest(apartmentDTO);
            }
            //if (apartmentDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            // mapujemy dane poniewaz musimy dodac DTO do bazy danych. DTO nie jest encja, więc EF Core nie wie jak to zapisać w SQL. Apartament to warstwa bazy danych DbSet<Apartment>
            // ApartmentDTO to warstwa API. Musimy zmapowaćDTO na model bazy danych (Apartment).
            //Używamy DTO, bo encje (model bazy) nie zawsze powinien wychodzić na zewnątrz API. DTO to filtr bezpieczeństwa i wygody między API, a bazą danych.
            Apartment model = new()
            {
                Name = apartmentDTO.Name,
                Details = apartmentDTO.Details,
                ImageUrl = apartmentDTO.ImageUrl,
                Occupancy = apartmentDTO.Occupancy,
                Rate = apartmentDTO.Rate,
                Sqft = apartmentDTO.Sqft,
                Amenity = apartmentDTO.Amenity
            };
            await _db.Apartments.AddAsync(model);
            await _db.SaveChangesAsync();

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
            var apartment = await _db.Apartments.FirstOrDefaultAsync(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            _db.Apartments.Remove(apartment);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody] ApartmentUpdateDTO apartmentDTO)
        {
            if (apartmentDTO == null || id != apartmentDTO.Id)
            {
                return BadRequest();
            }
            //var villa = ApartmentStore.apartmentList.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;

            Apartment model = new()
            {
                Name = apartmentDTO.Name,
                Details = apartmentDTO.Details,
                ImageUrl = apartmentDTO.ImageUrl,
                Occupancy = apartmentDTO.Occupancy,
                Rate = apartmentDTO.Rate,
                Sqft = apartmentDTO.Sqft,
                Amenity = apartmentDTO.Amenity,
                CreatedDate = DateTime.Now
            };
            _db.Apartments.Update(model);
            await _db.SaveChangesAsync();
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
            var apartment = await _db.Apartments.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            ApartmentUpdateDTO apartmentDTO = new()
            {
                Amenity = apartment.Amenity,
                Details = apartment.Details,
                Id = apartment.Id,
                ImageUrl = apartment.ImageUrl,
                Name = apartment.Name,
                Occupancy = apartment.Occupancy,
                Rate = apartment.Rate,
                Sqft = apartment.Sqft
            };
            if (apartment == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(apartmentDTO, ModelState);
            Apartment model = new Apartment()
            {
                Amenity = apartmentDTO.Amenity,
                Details = apartmentDTO.Details,
                Id = apartmentDTO.Id,
                ImageUrl = apartmentDTO.ImageUrl,
                Name = apartmentDTO.Name,
                Occupancy = apartmentDTO.Occupancy,
                Rate = apartmentDTO.Rate,
                Sqft = apartmentDTO.Sqft
            };
            _db.Apartments.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

