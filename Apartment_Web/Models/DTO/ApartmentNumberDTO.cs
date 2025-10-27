using System.ComponentModel.DataAnnotations;

namespace Apartment_Web.Models.DTO
{
    public class ApartmentNumberDTO
    {
        [Required]
        public int ApartmentNo { get; set; }
        [Required]
        public int ApartmentID { get; set; }

        public string SpecialDetails { get; set; }
        public ApartmentDTO Apartment { get; set; }
    }
}
