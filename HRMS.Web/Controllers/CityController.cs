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
        private readonly IRegionService regionService;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;

        public CityController(IRegionService regionService, ICountryService countryService, ICityService cityService)
        {
            this.regionService = regionService;
            this.countryService = countryService;
            this.cityService = cityService;
        }
        #region GET
        public async Task<IActionResult> Index()
        {
            var result = await regionService.GetAllAsync(null);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<CityViewModel>());
            }

            var countries = await countryService.GetAllAsync();
            if (!countries.IsSuccessful)
            {
                ModelState.AddModelError("", countries.Message);
                return View(new List<CityViewModel>());
            }
            var cities = await cityService.GetAllAsync(null, null);
            if (!cities.IsSuccessful)
            {
                ModelState.AddModelError("", cities.Message);
                return View(new List<CityViewModel>());
            }


            var list =
                (from region in result.Result
                 join country in countries.Result on region.CountryId equals country.Id
                 join city in cities.Result on region.Id equals city.RegionId
                 select new CityViewModel
                 {
                     Id = city.Id,
                     Name = city.Name,
                     Country = country.Name,
                     CountryId = region.CountryId,
                     Region = region.Name,
                     RegionId = region.Id
                 });

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
            return View(new CityViewModel
            {
                Id = id,
                Name = response.Result.Name,
                CountryId = response.Result.CountryId,
                Country = response.Result.Country.Name,
                Region = response.Result.Region.Name,
                RegionId = response.Result.RegionId
            });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var response = await cityService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CityViewModel());
            }

            return View(new CityViewModel
            {
                Id = id,
                Name = response.Result.Name,
                CountryId = response.Result.CountryId,
                Country = response.Result.Country.Name,
                Region = response.Result.Region.Name,
                RegionId = response.Result.RegionId
            });
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

                var toCreateModel = new City
                {
                    Name = model.Name,
                    CountryId = model.CountryId,
                    RegionId = model.RegionId
                };
                var result = await cityService.CreateAsync(toCreateModel);
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
                var toUpdateModel = new City
                {
                    Name = model.Name,
                    Id = model.Id,
                    CountryId = model.CountryId,
                    RegionId = model.RegionId
                };
                var result = await cityService.EditAsync(toUpdateModel);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            return View(model);

        }
    }
}