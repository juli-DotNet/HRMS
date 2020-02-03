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
    public class CountryController : Controller
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await countryService.GetAllAsync();
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
            }
            return View(result.Result.Select(a => new CountryViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Code = a.Code
            }));
        }

        public IActionResult Create()
        {
            var model = new CountryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var toCreateModel = new Country
                {
                    Name = model.Name,
                    Code = model.Code
                };
                var result = await countryService.CreateAsync(toCreateModel);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
            }


            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await countryService.GetByIdAsync(id);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new CountryViewModel());
            }

            return View(new CountryViewModel
            {
                Id = id,
                Name = result.Result.Name,
                Code = result.Result.Code
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var toUpdateModel = new Country
                {
                    Name = model.Name,
                    Id = model.Id,
                    Code = model.Code
                };
                var result = await countryService.EditAsync(toUpdateModel);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            return View(model);
              
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await countryService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CountryViewModel());
            }
            return View(new CountryViewModel
            {
                Id = id,
                Name = response.Result.Name,
                Code = response.Result.Code
            });
        }



        public async Task<IActionResult> Delete(int id)
        {

            var response = await countryService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

    }

}