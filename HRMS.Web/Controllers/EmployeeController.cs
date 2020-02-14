using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public async Task<ActionResult> Index()
        {
            var result = await employeeService.GetAllAsync(null);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<EmployeeViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }
        public ActionResult Create()
        {
            var model = new EmployeeViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {

                var toCreateModel = Parse(model);
                var result = await employeeService.CreateAsync(toCreateModel);
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
            var response = await employeeService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanyViewModel());
            }
            return View(Parse(response.Result));
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await employeeService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new CompanyViewModel());
            }
            return View(Parse(response.Result));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeService.EditAsync(Parse(model));

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
            var response = await employeeService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

        private EmployeeViewModel Parse(Employee a)
        {
            return new EmployeeViewModel
            {
                Id = a.Id,
                LastName = a.LastName,
                Name = a.Name,
                Telephone = a.Telephone,
                BirthDateDay = a.BirthDate.Day,
                BirthDateMonth = a.BirthDate.Month,
                BirthDateYear = a.BirthDate.Year,
                AddressId = a.AddressId,
                City = a.Address.City.Name,
                CityId = a.Address.CityId,
                Contact = a.Contact != null ? string.Concat(a.Contact.Name, ",", a.Contact.LastName) : "",
                ContactId = a.ContactId ?? Guid.Empty,
                Country = a.Address.Country.Name,
                CountryId = a.Address.CountryId,
                Email = a.Email,
                Mobile = a.Mobile,
                PostalCode = a.Address.PostalCode,
                Region = a.Address.Region.Name,
                RegionId = a.Address.RegionId,
                StreetName = a.Address.StreetName
            };
        }

        private Employee Parse(EmployeeViewModel a)
        {
            return new Employee
            {
                Id = a.Id,
                LastName = a.LastName,
                Name = a.Name,
                Telephone = a.Telephone,
                Email = a.Email,
                Mobile = a.Mobile,
                ContactId = a.ContactId,
                BirthDate = new DateTime(a.BirthDateYear, a.BirthDateMonth, a.BirthDateDay),
                AddressId = a.AddressId,
                Address = new Address
                {
                    Id = a.AddressId,
                    StreetName = a.StreetName,
                    PostalCode = a.PostalCode,
                    CityId = a.CityId,
                    RegionId = a.RegionId,
                    CountryId = a.CountryId
                }
            };
        }
    }
}