using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMS.Web.Models;
using HRMS.Core.Services.Interfaces;

namespace HRMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public readonly IUniOfWork _work;

        public HomeController(IUniOfWork work)
        {
            _work = work;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
