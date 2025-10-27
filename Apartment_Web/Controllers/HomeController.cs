using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Apartment_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;
        public HomeController(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<ApartmentDTO> list = new();
            var response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
       
    }
}
