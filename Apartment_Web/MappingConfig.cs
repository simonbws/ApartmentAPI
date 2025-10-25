
using Apartment_Web.Models.DTO;
using AutoMapper;

namespace Apartment_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<ApartmentDTO, ApartmentCreateDTO>().ReverseMap();
            CreateMap<ApartmentDTO, ApartmentUpdateDTO>().ReverseMap();

            CreateMap<ApartmentNumberDTO, ApartmentNumberCreateDTO>().ReverseMap();
            CreateMap<ApartmentNumberDTO, ApartmentNumberUpdateDTO>().ReverseMap();
        }
    }
}
