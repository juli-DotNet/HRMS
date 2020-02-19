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
    public class CompanySiteController : Controller
    {
        private readonly ICompanySiteService companySiteService;
        private readonly ICompanyService companyService;

        public CompanySiteController(ICompanySiteService companySiteService,ICompanyService companyService)
        {
            this.companySiteService = companySiteService;
            this.companyService = companyService;
        }

        public async Task<ActionResult> Index(Guid companyId)
        {
            ViewBag.companyId = companyId;
            var result = await companySiteService.GetAllAsync(companyId);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<CompanySiteViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }
        public async Task<ActionResult> Details(Guid id)
        {
           
            var response = await companySiteService.GetById(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanySiteViewModel());
            }
            ViewBag.companyId = response.Result.CompanyId;
            return View(Parse(response.Result));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
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

        private CompanySiteViewModel Parse(CompanySite a)
        {
            return new CompanySiteViewModel
            {
                Company = a.Company.Name,
                Id = a.Id,
                CompanyId = a.CompanyId,
                Site = a.Site.Name,
                SiteId = a.SiteId
            };
        }
    }
}