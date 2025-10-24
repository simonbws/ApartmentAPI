using Apartment_API.Models;
using Apartment_API.Models.DTO;
using AutoMapper;

namespace Apartment_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Apartment, ApartmentDTO>();
            CreateMap<ApartmentDTO, Apartment>();

            CreateMap<Apartment, ApartmentCreateDTO>().ReverseMap();
            CreateMap<Apartment, ApartmentUpdateDTO>().ReverseMap();
        }
    }
}
