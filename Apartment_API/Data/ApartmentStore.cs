using Apartment_API.Models.DTO;

namespace Apartment_API.Data
{
    public class ApartmentStore
    {
        public static List<ApartmentDTO> apartmentList = new List<ApartmentDTO>
        {
                new ApartmentDTO { Id = 1, Name="Pool View", Sqft = 100, Occupancy = 4},
                new ApartmentDTO { Id = 2, Name="Beach View", Sqft = 300, Occupancy = 3}
        };
    }
}
