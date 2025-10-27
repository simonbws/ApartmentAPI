using Apartment_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Apartment_Web.Models.View_Model
{
    public class ApartmentNumberCreateViewModel
    {
        public ApartmentNumberCreateViewModel()
        {
            ApartmentNumber = new ApartmentNumberCreateDTO();
        }
        public ApartmentNumberCreateDTO ApartmentNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ApartmentList { get; set; }
    }
}
