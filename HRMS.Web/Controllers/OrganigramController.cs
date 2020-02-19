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
    public class OrganigramController : Controller
    {
        private readonly IOrganigramService organigram;
        private readonly ICompanyService companyService;

        public OrganigramController(IOrganigramService organigram, ICompanyService companyService)
        {
            this.organigram = organigram;
            this.companyService = companyService;
        }
        public async Task<ActionResult> Index(Guid companyId)
        {
            ViewBag.companyId = companyId;
            var result = await organigram.GetAllAsync(companyId);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<OrganigramViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }

        public async Task<ActionResult> Create(Guid companyId)
        {
            var company = await companyService.GetByIdAsync(companyId);
            if (!company.IsSuccessful)
            {
                ModelState.AddModelError("", company.Message);
                return View(new OrganigramViewModel());
            }
            ViewBag.companyId = companyId;
            var model = new OrganigramViewModel() { CompanyId = companyId, Company = company.Result.Name };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrganigramViewModel model)
        {
            ViewBag.companyId = model.CompanyId;
            if (ModelState.IsValid)
            {
                var toCreateModel = Parse(model);
                var result = await organigram.CreateAsync(toCreateModel);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Edit", "Company", new { id = model.CompanyId });
            }
            return View(model);
        }
        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await organigram.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new OrganigramViewModel());
            }
            ViewBag.companyId = response.Result.CompanyId;
            return View(Parse(response.Result));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrganigramViewModel model)
        {
            ViewBag.companyId = model.CompanyId;
            if (ModelState.IsValid)
            {
                var result = await organigram.EditAsync(Parse(model));

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
            var response = await organigram.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new OrganigramViewModel());
            }
            ViewBag.companyId = response.Result.CompanyId;
            return View(Parse(response.Result));
        }



        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await organigram.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

        public async Task<ActionResult> DeleteHistory(Guid id)
        {
            var response = await organigram.DeleteEmployeeDetail(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }
        public async Task<ActionResult> EditHistory(Guid id)
        {
            var response = await organigram.GetCurrentEmployeeDetails(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new OrganigramEmployeeViewModel());
            }
            var model = Parse(response.Result);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditHistory(OrganigramEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await organigram.EditEmployee(Parse(model));

                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Edit", new { id = model.OrganigramId });
            }
            return View(model);
        }

        public async Task<ActionResult> EmployementHistory(Guid id)
        {
            var result = new EmployementJsonModel();
            var listResult = await organigram.GetOrganigramEmployeHistory(id);

            if (listResult.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = listResult.Result.Select(a => ParseJson(a));
            }
            else
            {
                result.ErrorMessage = listResult.Message;
            }
            return Json(result);
        }

        public async Task<ActionResult> AddEmployee(Guid id)
        {
            var result = new OrganigramEmployeeViewModel() { OrganigramId = id, StartDate = DateTime.Now };

            var response = await organigram.GetByIdAsync(id);
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
            }
            else
            {
                result.Organigram = response.Result.Name;
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee(OrganigramEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await organigram.AddEmployee(Parse(model));

                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Edit", new { id = model.OrganigramId });
            }
            return View(model);
        }

        #region Parsers
        private static EmployeeDto ParseJson(OrganigramEmployee model)
        {
            return new EmployeeDto
            {
                Id = model.Id,
                Name = string.Concat(model.Employee.Name, ":", model.Employee.LastName),
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                NetAmount = model.NetAmountInMonth,
                Amount = model.BrutoAmountInMonth
            };
        }
        private static OrganigramEmployee Parse(OrganigramEmployeeViewModel model)
        {
            return new OrganigramEmployee
            {
                Id = model.Id,
                OrganigramId = model.OrganigramId,
                EmployeeId = model.EmployeeId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                NetAmountInMonth = model.NetAmountInMonth,
                BrutoAmountInMonth = model.BrutoAmountInMonth
            };
        }
        private static OrganigramEmployeeViewModel Parse(OrganigramEmployee model)
        {
            return new OrganigramEmployeeViewModel
            {
                Id = model.Id,
                OrganigramId = model.OrganigramId,
                EmployeeId = model.EmployeeId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                NetAmountInMonth = model.NetAmountInMonth,
                BrutoAmountInMonth = model.BrutoAmountInMonth,
                Employee = model.Employee == null ? "" : string.Concat(model.Employee.Name, ":", model.Employee.LastName),
                Organigram = model.Organigram.Name
            };
        }
        private static Organigram Parse(OrganigramViewModel model)
        {
            return new Organigram
            {
                Name = model.Name,
                Id = model.Id,
                IsCeo = model.IsCeo,
                CompanySiteId = model.CompanySiteId,
                RespondsToId = model.RespondsToId,
                CompanyId = model.CompanyId
            };
        }
        private static OrganigramViewModel Parse(Organigram model)
        {
            return new OrganigramViewModel
            {
                Name = model.Name,
                Id = model.Id,
                IsCeo = model.IsCeo,
                CompanySiteId = model.CompanySiteId,
                CompanyId = model.CompanyId,
                RespondsToId = model.RespondsToId ?? Guid.Empty,
                CompanySite = model.CompanySite == null ? "" : string.Concat(model.CompanySite.Site.Name),
                RespondsTo = string.Concat(model.RespondsTo?.Name ?? ""),
                Company = model.Company.Name
            };
        }
        #endregion

    }
}