using System.ComponentModel.DataAnnotations;

namespace Apartment_API.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
