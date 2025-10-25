using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Apartment_API.Models
{
    public class ApartmentNumber
    {
        // apartment number is provided by the user, i.e. 102, 103, 104 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApartmentNo { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
