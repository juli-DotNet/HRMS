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
        public async Task<IActionResult> GetRegions(string search, int page, int? countryId)
        {

            var serviceResponse = await service.GetAllRegionsAsync(search, countryId);

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
        public async Task<IActionResult> GetCities(string search, int page, int? countryId, int? regionId)
        {

            var serviceResponse = await service.GetAllCitiesAsync(search, countryId, regionId);

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
        public async Task<IActionResult> GetSites(string search, int page, Guid companyId)
        {

            var serviceResponse = await service.GetAllSitesAsync(search, companyId);

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
        public IActionResult GetYears(string search, int page)
        {
            var last5Years = from n in Enumerable.Range(0, 80)
                             select DateTime.Now.Year - 18 - n;


            var result = new JsonGenericModel() { IsSuccessful = true };

            if (!string.IsNullOrEmpty(search))
            {
                last5Years = last5Years.Where(a => a.ToString().Contains(search.ToLower()));
            }
            result.Items = last5Years.Select(a => new SelectDataDTO { Id = a.ToString(), Text = a.ToString() });

            return Json(result);
        }
        [HttpGet]
        public IActionResult GetDays(string search, int page, int year, int month)
        {
            var result = new JsonGenericModel() { IsSuccessful = true };

            var date = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

            var list = new List<SelectDataDTO>();
            for (int i = 1; i <= date.Day; i++)
            {
                list.Add(new SelectDataDTO
                {
                    Id = i.ToString(),
                    Text = i.ToString()
                });
            }
            result.Items = list;
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts(string search, int page)
        {
            var serviceResponse = await service.GetAllContactsAsync(search);

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
        public async Task<IActionResult> GetCompanySites(string search, int page,Guid? companyId)
        {
            var serviceResponse = await service.GetCompanySitesAsync(search,companyId);

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
        public async Task<IActionResult> GetOrganigrams(string search, int page, Guid? companySiteId)
        {
            var serviceResponse = await service.GetOrganigramsAsync(search,companySiteId);

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
        private SelectDataDTO Parse(CompanySite a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = string.Concat(a.Company.Name,":",a.Site.Name)
            };
        }

        private SelectDataDTO Parse(Organigram a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }

        private SelectDataDTO Parse(Employee a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = string.Concat(a.Name, ",", a.LastName)
            };
        }

        private SelectDataDTO Parse(Site a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }

        private SelectDataDTO Parse(City a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }
        private SelectDataDTO Parse(Region a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }

        private static SelectDataDTO Parse(Country a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }
    }
}