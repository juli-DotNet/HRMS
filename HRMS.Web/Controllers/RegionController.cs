using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService regionService;

        public RegionController(IRegionService regionService)
        {
            this.regionService = regionService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}