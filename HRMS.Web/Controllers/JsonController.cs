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
    public class JsonController : Controller
    {
        private readonly IJsonService service;

        public JsonController(IJsonService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetCountries(string search, int page)
        {

            var serviceResponse = await service.GetAllCountriesAsync(search);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => ParseCountries(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetRegions(string search, int page)
        {

            var serviceResponse = await service.GetAllRegionsAsync(search,null);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => ParseCountries(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }

        private JsonData ParseCountries(Region a)
        {
            return new JsonData
            {
                Id = a.Id.ToString(),
                Text = a.Name

            };
        }

        private static JsonData ParseCountries(Core.Model.Country a)
        {
            return new JsonData
            {
                Id = a.Id.ToString(),
                Text = a.Name

            };
        }
    }
}