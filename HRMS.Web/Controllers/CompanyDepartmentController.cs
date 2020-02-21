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
    public class CompanyDepartmentController : Controller
    {
        private readonly ICompanyDepartamentService companyDepartamentService;
        private readonly ICompanyService companyService;

        public CompanyDepartmentController(ICompanyDepartamentService companyDepartamentService,ICompanyService companyService)
        {
            this.companyDepartamentService = companyDepartamentService;
            this.companyService = companyService;
        }

        public async Task<ActionResult> Index(Guid companyId)
        {
            ViewBag.companyId = companyId;
            var result = await companyDepartamentService.GetAllAsync(companyId);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<CompanyDepartmentViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }
        public async Task<ActionResult> Details(Guid id)
        {
           
            var response = await companyDepartamentService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanyDepartmentViewModel());
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

            var linkResult = await companyDepartamentService.DeleteAsync(id);

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
        public async Task<ActionResult> LinkDepartment(Guid id, Guid siteId)
        {
            GenericViewModel result = new GenericViewModel();
            if (siteId == Guid.Empty)
            {
                result.ErrorMessage = "Please select site";
                return Json(result);
            }

            var linkResult = await companyDepartamentService.CreateAsync(id, siteId);

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
      

        //public async Task<ActionResult> LinkedSites(Guid id)
        //{
        //    var result = new LinkedSiteJsonModel();
        //    var linkResult = await companyService.GetByIdAsync(id);

        //    if (linkResult.IsSuccessful)
        //    {
        //        result.IsSuccessful = true;
        //        result.Items = linkResult.Result.Select(a => Parse(a));
        //    }
        //    else
        //    {
        //        result.ErrorMessage = linkResult.Message;
        //    }
        //    return Json(result);
        //}

        //private DepartmentDto Parse(CompanyDepartament a)
        //{
        //    return new DepartmentDto
        //    {
        //        City = a.Site.Address.City.Name,
        //        Region = a.Site.Address.Region.Name,
        //        Country = a.Site.Address.Country.Name,
        //        PostalCode = a.Site.Address.PostalCode,
        //        Name = a.Name,
        //        Id = a.Id
        //    };
        //}
        private CompanyDepartmentViewModel Parse(CompanyDepartament a)
        {
            return new CompanyDepartmentViewModel
            {
                Company = a.Company.Name,
                Id = a.Id,
                CompanyId = a.CompanyId,
                Site = a.Departament.Name,
                SiteId = a.DepartamentId
            };
        }
    }
}