using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class RegionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}