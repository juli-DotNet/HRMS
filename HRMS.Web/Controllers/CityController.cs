using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class CityController : Controller
    {

        private readonly ICityService cityService;

        public CityController( ICityService cityService)
        {
            this.cityService = cityService;
        }
        #region GET
        public async Task<IActionResult> Index()
        {
            
            var cities = await cityService.GetAllAsync(null, null);
            if (!cities.IsSuccessful)
            {
                ModelState.AddModelError("", cities.Message);
                return View(new List<CityViewModel>());
            }
            var list = cities.Result.Select(a => Parse(a));
            return View(list);
        }

        public IActionResult Create()
        {
            var model = new CityViewModel();
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await cityService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CityViewModel());
            }
            return View(Parse(response.Result));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var response = await cityService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CityViewModel());
            }

            return View(Parse(response.Result));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var response = await cityService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await cityService.CreateAsync(Parse(model));
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await cityService.EditAsync(Parse(model));
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            return View(model);

        }
        private static CityViewModel Parse(City response)
        {
            return new CityViewModel
            {
                Id = response.Id,
                Name = response.Name,
                CountryId = response.CountryId,
                Country = response.Country.Name,
                Region = response.Region.Name,
                RegionId = response.RegionId
            };
        }
        private static City Parse(CityViewModel model)
        {
            return new City
            {
                Name = model.Name,
                Id = model.Id,
                CountryId = model.CountryId,
                RegionId = model.RegionId
            };
        }
    }
}