using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Services.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }
        public IActionResult Index()
        {
            var list = countryService.GetAll();
            return View(list.Select(a => new CountryViewModel
            {
                Id = a.Id,
                Name = a.Name
            }));
        }
    }

}