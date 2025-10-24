using Apartment_API.Data;
using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Apartment_API.Controllers
{
    [Route("api/ApartmentAPI")]
    [ApiController]
    public class ApartmentAPIController : ControllerBase
    {
        private readonly ILogger<ApartmentAPIController> _logger;

        public ApartmentAPIController(ILogger<ApartmentAPIController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApartmentDTO> GetApartments()
        {
            _logger.LogInformation("Getting all apartments");
            return Ok(ApartmentStore.apartmentList);
        }
        [HttpGet("{id:int}", Name = "GetApartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApartmentDTO> GetApartments(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get apartment Error with Ii" + id);
                return BadRequest();
            }
            var apartment = ApartmentStore.apartmentList.FirstOrDefault(u => u.Id == id);
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
        public ActionResult<ApartmentDTO> CreateApartment([FromBody] ApartmentDTO apartmentDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState); 
            //}
            if(ApartmentStore.apartmentList.FirstOrDefault(u=> u.Name.ToLower()==apartmentDTO.Name.ToLower())!=null) 
            {
                ModelState.AddModelError("CustomError", "Apartment already Exists!");
                return BadRequest(ModelState);
            }
            if(apartmentDTO == null)
            {
                return BadRequest(apartmentDTO);
            }
            if (apartmentDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            apartmentDTO.Id = ApartmentStore.apartmentList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            ApartmentStore.apartmentList.Add(apartmentDTO);

            return CreatedAtRoute("GetApartment", new { id = apartmentDTO.Id }, apartmentDTO);
        }
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteApartment")]
        public ActionResult<ApartmentDTO> DeleteApartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var apartment = ApartmentStore.apartmentList.FirstOrDefault(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            ApartmentStore.apartmentList.Remove(apartment);
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateApartment(int id, [FromBody] ApartmentDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = ApartmentStore.apartmentList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();

        }

    }
}

