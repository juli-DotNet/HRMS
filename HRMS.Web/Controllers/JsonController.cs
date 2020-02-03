using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
          
            var serviceResponse = await service.GetAllCountriesAsync();

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Where(a=>a.Name.ToLower().Contains(search.ToLower())).Select(a => ParseCountries(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
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