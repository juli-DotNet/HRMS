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
    public class SiteController : Controller
    {
        private readonly ISiteService siteService;

        public SiteController(ISiteService siteService)
        {
            this.siteService = siteService;
        }

        public async Task<ActionResult> Index()
        {
            var result = await siteService.GetAllAsync();
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<SiteViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }

        public ActionResult Create()
        {
            var model = new SiteViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SiteViewModel model)
        {
            if (ModelState.IsValid)
            {

                var toCreateModel = Parse(model);
                var result = await siteService.CreateAsync(toCreateModel);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var response = await siteService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanyViewModel());
            }
            return View(Parse(response.Result));
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await siteService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanyViewModel());
            }
            return View(Parse(response.Result));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await siteService.EditAsync(Parse(model));

                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await siteService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

        private static Site Parse(SiteViewModel model)
        {
            return new Site
            {
                Name = model.Name,
                Id = model.Id,
                Address = new Address
                {

                    Id = model.AddressId,
                    StreetName = model.StreetName,
                    PostalCode = model.PostalCode,
                    CountryId = model.CountryId,
                    RegionId = model.RegionId,
                    CityId = model.CityId

                }
            };
        }
        private static SiteViewModel Parse(Site model)
        {
            return new SiteViewModel
            {
                Name = model.Name,
                Id = model.Id,
                City = model.Address.City.Name,
                CityId = model.Address.CityId,
                Country = model.Address.Country.Name,
                CountryId = model.Address.CountryId,
                Region = model.Address.Region.Name,
                RegionId = model.Address.RegionId,
                AddressId = model.AddressId,
                PostalCode = model.Address.PostalCode,
                StreetName = model.Address.StreetName
            };
        }

    }
}