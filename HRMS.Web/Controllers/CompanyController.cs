using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public async Task<ActionResult> Index()
        {
            var result = await companyService.GetAllAsync();
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<CompanyViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }
        public ActionResult Create()
        {
            var model = new CompanyViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {

                var toCreateModel = Parse(model);
                var result = await companyService.CreateAsync(toCreateModel);
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
            var response = await companyService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanyViewModel());
            }
            return View(Parse(response.Result));
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await companyService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanyViewModel());
            }
            return View(Parse(response.Result));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await companyService.EditAsync(Parse(model));

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
        public async Task<ActionResult> LinkSite(Guid id, Guid siteId)
        {
            GenericViewModel result = new GenericViewModel();
            if (siteId == Guid.Empty)
            {
                result.ErrorMessage = "Please select site";
                return Json(result);
            }

            var linkResult = await companyService.LinkSiteAsync(id, siteId);

            if (linkResult.IsSuccessful)
            {
                result.IsSuccessful = true;
            }
            else
            {
                result.ErrorMessage = linkResult.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public async Task<ActionResult> RemoveLinkedSite(Guid id)
        {
            GenericViewModel result = new GenericViewModel();
            if (id == Guid.Empty)
            {
                result.ErrorMessage = "Please select site";
            }

            var linkResult = await companyService.RemoveLinkedSite(id);

            if (linkResult.IsSuccessful)
            {
                result.IsSuccessful = true;
            }
            else
            {
                result.ErrorMessage = linkResult.Message;
            }
            return Json(result);
        }

        public async Task<ActionResult> LinkedSites(Guid id)
        {
            var result = new LinkedSiteJsonModel();
            var linkResult = await companyService.GetLinkedSites(id);

            if (linkResult.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = linkResult.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = linkResult.Message;
            }
            return Json(result);
        }

        private SiteDto Parse(CompanySite a)
        {
            return new SiteDto
            {
                City = a.Site.Address.City.Name,
                Region = a.Site.Address.Region.Name,
                Country = a.Site.Address.Country.Name,
                PostalCode = a.Site.Address.PostalCode,
                Name = a.Site.Name,
                Id = a.Id
            };
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await companyService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

        private static Company Parse(CompanyViewModel model)
        {
            return new Company
            {
                Name = model.Name,
                Id = model.Id,
                NIPT = model.NIPT,
                Description = model.Description
            };
        }
        private static CompanyViewModel Parse(Company model)
        {
            return new CompanyViewModel
            {
                Name = model.Name,
                Id = model.Id,
                NIPT = model.NIPT,
                Description = model.Description
            };
        }

    }
}