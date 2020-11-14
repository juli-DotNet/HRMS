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
    public class DepartmentController : Controller
    {
        private readonly IDepartamentService siteService;

        public DepartmentController(IDepartamentService siteService)
        {
            this.siteService = siteService;
        }

        public async Task<ActionResult> Index()
        {
            var result = await siteService.GetAllAsync();
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<DepartmentViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }

        public ActionResult Create()
        {
            var model = new DepartmentViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DepartmentViewModel model)
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
        public async Task<ActionResult> Edit(DepartmentViewModel model)
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

        private static Departament Parse(DepartmentViewModel model)
        {
            return new Departament
            {
                Name = model.Name,
                Id = model.Id
            };
        }
        private static DepartmentViewModel Parse(Departament model)
        {
            return new DepartmentViewModel
            {
                Name = model.Name,
                Id = model.Id
            };
        }

    }
}