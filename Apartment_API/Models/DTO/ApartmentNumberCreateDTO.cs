using System.ComponentModel.DataAnnotations;

namespace Apartment_API.Models.DTO
{
    public class ApartmentNumberCreateDTO
    {
        [Required]
        public int ApartmentNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
