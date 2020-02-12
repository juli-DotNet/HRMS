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

        public OrganigramController(IOrganigramService organigram)
        {
            this.organigram = organigram;
        }
        public async Task<ActionResult> Index()
        {
            var result = await organigram.GetAllAsync(null);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(new List<OrganigramViewModel>());
            }
            var list = result.Result.Select(a => Parse(a));
            return View(list);

        }

        public ActionResult Create()
        {
            var model = new OrganigramViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrganigramViewModel model)
        {
            if (ModelState.IsValid)
            {

                var toCreateModel = Parse(model);
                var result = await organigram.CreateAsync(toCreateModel);
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
            return View(Parse(response.Result));
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var response = await organigram.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new OrganigramViewModel());
            }
            return View(Parse(response.Result));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrganigramViewModel model)
        {
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

        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await organigram.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

        private static Organigram Parse(OrganigramViewModel model)
        {
            return new Organigram
            {
                Name = model.Name,
                Id = model.Id,
                IsCeo = model.IsCeo,
                CompanySiteId = model.CompanySiteId,
                RespondsToId = model.RespondsToId
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
                RespondsToId = model.RespondsToId ?? Guid.Empty,
                CompanySite = string.Concat(model.CompanySite.Company, ":", model.CompanySite.Site.Name),
                RespondsTo = string.Concat(model.RespondsTo.Name)
            };
        }
    }
}