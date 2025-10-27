using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Models.ViewModel;
using Apartment_Web.Services;
using Apartment_Web.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apartment_Web.Controllers
{
    public class ApartmentNumberController : Controller
    {
        private readonly IApartmentNumberService _apartmentNumberService;
        private readonly IMapper _mapper;
        private readonly IApartmentService _apartmentService;
        public ApartmentNumberController(IApartmentNumberService apartmentNumberService, IMapper mapper, IApartmentService apartmentService)
        {
            _apartmentNumberService = apartmentNumberService;
            _mapper = mapper;
            _apartmentService = apartmentService;
        }
        public async Task<IActionResult> IndexApartmentNumber()
        {
            List<ApartmentNumberDTO> list = new();
            var response = await _apartmentNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ApartmentNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        //
        public async Task<IActionResult> CreateApartmentNumber()
        {
            // that will populate the drowdown which will be IEnumerable of Select List Item
            ApartmentNumberCreateViewModel apartmentNumberViewModel = new ApartmentNumberCreateViewModel();
            var response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                apartmentNumberViewModel.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(apartmentNumberViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApartmentNumber(ApartmentNumberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                var response = await _apartmentNumberService.CreateAsync<APIResponse>(model.ApartmentNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexApartmentNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _apartmentService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(model);
        }
        public async Task<IActionResult> UpdateApartmentNumber(int apartmentNo)
        {
            ApartmentNumberUpdateViewModel apartmentNumberViewModel = new ApartmentNumberUpdateViewModel();
            var response = await _apartmentService.GetAsync<APIResponse>(apartmentNo);
            if (response != null && response.IsSuccess)
            {
                ApartmentNumberDTO model = JsonConvert.DeserializeObject<ApartmentNumberDTO>(Convert.ToString(response.Result));
                apartmentNumberViewModel.ApartmentNumber = _mapper.Map<ApartmentNumberUpdateDTO>(model);
            }

            response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                apartmentNumberViewModel.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            return View(apartmentNumberViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApartmentNumber(ApartmentNumberUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {

                var response = await _apartmentNumberService.UpdateAsync<APIResponse>(model.ApartmentNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexApartmentNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _apartmentService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteApartmentNumber(int apartmentNo)
        {
            ApartmentNumberDeleteViewModel apartmentNumberViewModel = new ApartmentNumberDeleteViewModel();
            var response = await _apartmentService.GetAsync<APIResponse>(apartmentNo);
            if (response != null && response.IsSuccess)
            {
                ApartmentNumberDTO model = JsonConvert.DeserializeObject<ApartmentNumberDTO>(Convert.ToString(response.Result));
                apartmentNumberViewModel.ApartmentNumber = model;
            }

            response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                apartmentNumberViewModel.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(apartmentNumberViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteApartmentNumber(ApartmentNumberDeleteViewModel model) // we receive apartmentdto here
        {

            var response = await _apartmentNumberService.DeleteAsync<APIResponse>(model.ApartmentNumber.ApartmentNo);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexApartmentNumber));
            }

            return View(model);
        }

    }
}
