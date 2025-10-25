using System.ComponentModel.DataAnnotations;

namespace Apartment_API.Models.DTO
{
    public class ApartmentNumberUpdateDTO
    {
        [Required]
        public int ApartmentNo { get; set; }
        [Required]
        public int ApartmentID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
