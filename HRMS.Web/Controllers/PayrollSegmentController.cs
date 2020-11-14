using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class PayrollSegmentController : Controller
    {
        private readonly IPayrollSegmentService segmentService;
        private readonly IPayrollSeasonService seasonService;

        public PayrollSegmentController(IPayrollSegmentService segmentService, IPayrollSeasonService seasonService)
        {
            this.segmentService = segmentService;
            this.seasonService = seasonService;
        }
        public async Task<IActionResult> Index(int seasonId)
        {
            ViewBag.SeasonId = seasonId;
            var listResult = await segmentService.GetAllAsync(seasonId);
            if (!listResult.IsSuccessful)
            {
                ModelState.AddModelError("", listResult.Message);
                return View(new List<SegmentViewModel>());
            }
            var list = listResult.Result.Select(a => Parse(a));
            return View(list);
        }

        public async Task<IActionResult> Create(int seasonId)
        {
            ViewBag.SeasonId = seasonId;
            var seasonResult = await seasonService.GetByIdAsync(seasonId);
            if (!seasonResult.IsSuccessful)
            {
                ModelState.AddModelError("", seasonResult.Message);
                return View(new SegmentViewModel());
            }
            var model = new SegmentViewModel() { PayrollSeasonId = seasonId, PayrollSeason = seasonResult.Result.Name };
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await segmentService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new SegmentViewModel());
            }
            return View(Parse(response.Result));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var response = await segmentService.GetByIdAsync(id);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(new SegmentViewModel());
            }

            return View(Parse(response.Result));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var response = await segmentService.DeleteAsync(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.Message
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SegmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await segmentService.CreateAsync(Parse(model));
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                return RedirectToAction("Index",new { seasonId=model.PayrollSeasonId});
            }
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SegmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await segmentService.EditAsync(Parse(model));
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);

                }
                return RedirectToAction("Index", new { seasonId = model.PayrollSeasonId });
            }
            return View(model);

        }
        private static SegmentViewModel Parse(PayrollSegment response)
        {
            return new SegmentViewModel
            {
                Id = response.Id,
                Name = response.Name,
                Nr = response.Nr,
                PayrollSeason = response.PayrollSeason?.Name,
                PayrollSeasonId = response.PayrollSeasonId
            };
        }
        private static PayrollSegment Parse(SegmentViewModel model)
        {
            return new PayrollSegment
            {
                Name = model.Name,
                Id = model.Id,
                Nr = model.Nr,
                PayrollSeasonId = model.PayrollSeasonId
            };
        }
    }
}