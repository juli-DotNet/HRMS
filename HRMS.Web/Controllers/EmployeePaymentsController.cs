using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace HRMS.Web.Controllers
{
    public class EmployeePaymentsController : Controller
    {
        private readonly IPayrollService service;
        private readonly ICompanyService companyService;

        public EmployeePaymentsController(IPayrollService service, ICompanyService companyService)
        {
            this.service = service;
            this.companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid Id)
        {
            var listResult = await service.GetGeneratedPayrolls(Id);
            ViewBag.CompanyId = Id;
            if (!listResult.IsSuccessful)
            {
                ModelState.AddModelError("", listResult.Message);
                return View(new List<CompanyPayrollViewModel>());
            }

            return View(listResult.Result.Select(Parse));
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid Id)
        {
            var companyResult = await companyService.GetByIdAsync(Id);

            if (!companyResult.IsSuccessful)
            {
                ModelState.AddModelError("", companyResult.Message);
            }
            return View(new CompanyPayrollViewModel()
            {
                CompanyId = Id,
                CompanyName = companyResult.Result?.Name
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var companyResult = await service.GetByIdAsync(Id);

            if (!companyResult.IsSuccessful)
            {
                ModelState.AddModelError("", companyResult.Message);
                return View(new CompanyPayrollViewModel()
                {
                    Id = Id
                });
            }
            return View(Parse(companyResult.Result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyPayrollViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.GenerateAsync(model.CompanyId, model.SegmentId);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Index", new { Id = model.CompanyId });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyPayrollViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.PayCompanyPayrollAsync(model.Id, model.IsPayed);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Index", new { Id = model.CompanyId });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            GenericViewModel result = new GenericViewModel();
            if (id == Guid.Empty)
            {
                result.ErrorMessage = "Please select payment";
            }

            var linkResult = await service.DeleteCompanyPayrollAsync(id);

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
        public async Task<IActionResult> GetPayrollEmployees(Guid id)
        {
            var result = await service.GetPayrollEmployees(id);

            if (result.IsSuccessful)
                return Json(result.Result.Select(Parse));
            return Json("Error happened");
        }
        private static PayrollEmployeetDto Parse(EmployeeCompanyPayroll model)
        {
            return new PayrollEmployeetDto
            {
                Id = model.Id,
                Amount = model.BrutoAmount,
                Employee = $"{model.Employee.Name} {model.Employee.LastName}",
                EndDate = model.OrganigramEmployee.EndDate,
                StartDate = model.OrganigramEmployee.StartDate,
                Neto = model.NetoAmount,
                Position = model.OrganigramEmployee.Organigram.Name
            };
        }
        private static CompanyPayrollViewModel Parse(CompanyPayroll model)
        {
            return new CompanyPayrollViewModel
            {
                Id = model.Id,
                IsPayed = model.IsPayed,
                CompanyName = model.Company.Name,
                Segment = model.PayrollSegment.Name,
                TotalAmount = model.TotalAmounBruto,
                Season = model.PayrollSegment.PayrollSeason.Name,
                CompanyId = model.CompanyId,
                SeasonId = model.PayrollSegment.PayrollSeasonId,
                SegmentId = model.PayrollSegmentId
            };
        }

    }
}