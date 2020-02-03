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
    public class RegionController : Controller
    {
        private readonly IRegionService regionService;
        private readonly ICountryService countryService;

        public RegionController(IRegionService regionService, ICountryService countryService)
        {
            this.regionService = regionService;
            this.countryService = countryService;
        }
        #region GET
        public async Task<IActionResult> Index()
        {
            var result = await regionService.GetAllAsync(null);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<RegionViewModel>());
            }

            var countries = await countryService.GetAllAsync();
            if (!countries.IsSuccessful)
            {
                ModelState.AddModelError("", countries.Message);
                return View(new List<RegionViewModel>());
            }

            var list =
                (from region in result.Result
                 join country in countries.Result on region.CountryId equals country.Id
                 select new RegionViewModel
                 {
                     Id = region.Id,
                     Name = region.Name,
                     Country = country.Name,
                     CountryId = region.CountryId
                 });

            return View(list);


        }

        public IActionResult Create()
        {
            var model = new RegionViewModel();
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await regionService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new RegionViewModel());
            }
            return View(new RegionViewModel
            {
                Id = id,
                Name = response.Result.Name,
                CountryId = response.Result.CountryId,
                Country = response.Result.Country.Name
            });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await regionService.GetByIdAsync(id);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new RegionViewModel());
            }

            return View(new RegionViewModel
            {
                Id = id,
                Name = result.Result.Name,
                CountryId = result.Result.CountryId,
                Country=result.Result.Country.Name
            });
        }
        public async Task<IActionResult> Delete(int id)
        {

            var response = await regionService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

        #endregion

        #region POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegionViewModel model)
        {
            if (ModelState.IsValid)
            {

                var toCreateModel = new Region
                {
                    Name = model.Name,
                    CountryId = model.CountryId
                };
                var result = await regionService.CreateAsync(toCreateModel);
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
        public async Task<IActionResult> Edit(RegionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var toUpdateModel = new Region
                {
                    Name = model.Name,
                    Id = model.Id,
                    CountryId = model.CountryId
                };
                var result = await regionService.EditAsync(toUpdateModel);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            return View(model);

        }
        #endregion




    }
}