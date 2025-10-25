using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Apartment_Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;
        public ApartmentController(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexApartment()
        {
            List<ApartmentDTO> list = new();
            var response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
        public async Task<IActionResult> CreateApartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(ApartmentCreateDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _apartmentService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexApartment));
                }
            }
            return View(model);
        }
    }
}
