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
                result.Items = serviceResponse.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetRegions(string search, int page,int? countryId)
        {

            var serviceResponse = await service.GetAllRegionsAsync(search,countryId);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(string search, int page, int? countryId,int? regionId)
        {

            var serviceResponse = await service.GetAllCitiesAsync(search, countryId,regionId);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }

        private JsonData Parse(City a)
        {
            return new JsonData
            {
                Id = a.Id.ToString(),
                Text = a.Name

            };
        }
        private JsonData Parse(Region a)
        {
            return new JsonData
            {
                Id = a.Id.ToString(),
                Text = a.Name

            };
        }

        private static JsonData Parse(Country a)
        {
            return new JsonData
            {
                Id = a.Id.ToString(),
                Text = a.Name

            };
        }
    }
}