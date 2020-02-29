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
    public class PayrollSeasonController : Controller
    {
        private readonly IPayrollSeasonService seasonService;
        private readonly IPayrollSegmentService segmentService;

        public PayrollSeasonController(IPayrollSeasonService seasonService, IPayrollSegmentService segmentService)
        {
            this.seasonService = seasonService;
            this.segmentService = segmentService;
        }
        public async Task<IActionResult> Index()
        {

            var listResult = await seasonService.GetAllAsync();
            if (!listResult.IsSuccessful)
            {
                ModelState.AddModelError("", listResult.Message);
                return View(new List<SeasonViewModel>());
            }
            var list = listResult.Result.Select(a => Parse(a));
            return View(list);
        }

        public IActionResult Create()
        {
            var model = new SeasonViewModel();
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await seasonService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new SeasonViewModel());
            }
            return View(Parse(response.Result));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var response = await seasonService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new SeasonViewModel());
            }

            return View(Parse(response.Result));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var response = await seasonService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }

        public async Task<IActionResult> GetSegments(int id)
        {
            var result = new SegmentJsonModel();
            var response = await segmentService.GetAllAsync(id);

            if (response.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = response.Result.Select(a => ParseJson(a));
            }
            else
            {
                result.ErrorMessage = response.Message;
            }
            return Json(result);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await seasonService.CreateAsync(Parse(model));
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SeasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await seasonService.EditAsync(Parse(model));
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            return View(model);

        }
        private static SeasonViewModel Parse(PayrollSeason response)
        {
            return new SeasonViewModel
            {
                Id = response.Id,
                Name = response.Name,
                Year = response.year
            };
        }
        private static PayrollSeason Parse(SeasonViewModel model)
        {
            return new PayrollSeason
            {
                Name = model.Name,
                Id = model.Id,
                year = model.Year
            };
        }

        private SegmentDTO ParseJson(PayrollSegment a)
        {
            return new SegmentDTO
            {
                Id = a.Id,
                Name = a.Name,
                Nr = a.Nr
            };
        }
    }
}