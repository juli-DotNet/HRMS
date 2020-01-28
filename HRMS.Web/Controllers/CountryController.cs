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
        public IActionResult Index()
        {
            var list = countryService.GetAll();
            return View(list.Select(a => new CountryViewModel
            {
                Id = a.Id,
                Name = a.Name
            }));
        }
        public IActionResult Create()
        {
            var model = new CountryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CountryViewModel model)
        {
            var toCreateModel = new Country
            {
                Name = model.Name
            };
            countryService.Create(toCreateModel);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var model = countryService.GetById(id);
            return View(new CountryViewModel {
                Id=id,
                Name=model.Name
            });
        }

        public IActionResult Details(int id)
        {
            var model = countryService.GetById(id);
            return View(new CountryViewModel
            {
                Id = id,
                Name = model.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CountryViewModel model)
        {
            var toUpdateModel = new Country
            {
                Name = model.Name,
                Id = model.Id
            };
            countryService.Edit(toUpdateModel);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            countryService.Delete(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = true
            });
        }

    }

}