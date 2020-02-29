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
        private readonly ICompanyService companyService;
        private readonly ICompanyDepartamentService companyDepartmentService;

        public JsonController(IJsonService service, ICompanyService companyService, ICompanyDepartamentService companySiteService)
        {
            this.service = service;
            this.companyService = companyService;
            this.companyDepartmentService = companySiteService;
        }
        #region reference
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
        public async Task<IActionResult> GetDepartments(string search, int page, Guid companyId)
        {

            var serviceResponse = await service.GetAllDepartmentsAsync(search, companyId);

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
        public async Task<IActionResult> GetPayrollSeasons(string search, int page)
        {

            var serviceResponse = await service.GetPayrollSeasonsAsync(search);

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
        public async Task<IActionResult> GetCompanySites(string search, int page, Guid? companyId)
        {
            var serviceResponse = await service.GetCompanyDepartmentsAsync(search, companyId);

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
            var serviceResponse = await service.GetOrganigramsAsync(search, companySiteId);

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
        #endregion











        [HttpGet]
        public async Task<IActionResult> GetCompanyOrganigrams(Guid companyId)
        {
            var result = new CompanyOrganigramJsonModel();

            var organigramResponse = await service.GetCompanyOrganigramsAsync(companyId);
            var employeeResponse = await service.GetCompanyEmployesAsync(companyId);
            var companyResponse = await companyService.GetByIdAsync(companyId);
            var companySites = await companyDepartmentService.GetAllAsync(companyId);

            if (organigramResponse.IsSuccessful && employeeResponse.IsSuccessful && companyResponse.IsSuccessful && companySites.IsSuccessful)
            {
                result.IsSuccessful = true;

                var items = (from org in organigramResponse.Result
                             join emp in employeeResponse.Result.Where(a => !a.EndDate.HasValue || a.EndDate.Value >= DateTime.Now) on org.Id equals emp.OrganigramId into ps
                             from dt in ps.DefaultIfEmpty()
                             select new
                             {
                                 org.Id,
                                 Name = dt?.Employee == null ? "" : string.Join(dt.Employee.Name, " : ", dt.Employee.LastName),
                                 Title = org.Name,
                                 RespondsTo = org.RespondsToId,
                                 org.IsCeo,
                                 org.CompanyDepartament,
                                 org.CompanyDepartamentId
                             }).ToList();
                var list = new List<OrganigramDto>();
                // add the company
                var company = new OrganigramDto
                {
                    Id = Guid.Empty,
                    Name = companyResponse.Result.Name,
                    Title = "Company",
                    Children = new List<OrganigramDto>()
                };

                var ceoData = items.FirstOrDefault(a => a.CompanyDepartamentId == null && a.IsCeo);

                var companyCeo = new OrganigramDto() { Id = ceoData.Id, Name = ceoData.Name, Title = ceoData.Title, Children = new List<OrganigramDto>() };

                list.Add(companyCeo);

                //Bord
                foreach (var org in items.Where(a => !a.CompanyDepartamentId.HasValue && !a.IsCeo && a.RespondsTo == null))
                {
                    var dt = new OrganigramDto
                    {
                        Id = org.Id,
                        Name = org.Name,
                        Title = org.Title,
                        Children = new List<OrganigramDto>()
                    };
                    company.Children.Add(dt);
                    list.Add(dt);
                }
                //add the children TDDDD
                var retryList = new List<OrganigramDto>();

                foreach (var item in items.Where(a => !a.CompanyDepartamentId.HasValue && !a.IsCeo && a.RespondsTo != null))
                {
                    var father = list.FirstOrDefault(a => a.Id == item.RespondsTo);
                    var dt = new OrganigramDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Title = item.Title,
                        Children = new List<OrganigramDto>(),
                        ParentId = item.RespondsTo ?? Guid.Empty
                    };
                    if (father != null)
                    {

                        father.Children.Add(dt);
                        list.Add(dt);
                    }
                    else
                    {
                        if (items.Any(a => a.Id == item.RespondsTo))// if we have a father
                        {
                            retryList.Add(dt);
                        }
                    }

                }

                while (retryList.Count > 0)
                {
                    var tmplist = retryList.ToList();
                    foreach (var ch in tmplist)
                    {
                        var father = list.FirstOrDefault(a => a.Id == ch.ParentId);

                        if (father != null)
                        {
                            list.Add(ch);// to keep the search simplier
                            father.Children.Add(ch);
                            retryList.Remove(ch);
                        }
                    }
                }

                // add an empty node to linke the ceo
                company.Children.Add(new OrganigramDto
                {

                    Id = Guid.Empty,
                    Name = "",
                    Title = "Workers",
                    Children = new List<OrganigramDto>() { companyCeo }
                });

                //site Ceos
                foreach (var cite in companySites.Result)
                {
                    companyCeo.Children.Add(new OrganigramDto
                    {
                        Id = cite.Id,
                        Name = cite.Departament.Name,
                        Title = "Site Ceo"
                    });
                }

                list.Clear();


                result.Items = new List<OrganigramDto> { company };
            }
            else
            {
                result.ErrorMessage = organigramResponse.Message;
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyDepartmentOrganigrams(Guid companyDeparmentId)
        {
            var result = new CompanyOrganigramJsonModel();



            var companySiteResponse = await companyDepartmentService.GetByIdAsync(companyDeparmentId);

            if (companySiteResponse.IsSuccessful)
            {
                result.IsSuccessful = true;

                var employeeResponse = await service.GetCompanyEmployesAsync(companySiteResponse.Result.CompanyId);
                var organigramResponse = await service.GetCompanyOrganigramsAsync(companySiteResponse.Result.CompanyId);

                if (employeeResponse.IsSuccessful && organigramResponse.IsSuccessful)
                {
                    var employees = employeeResponse.Result.Where(a => a.Organigram.CompanyDepartamentId == companyDeparmentId && (a.EndDate==null || a.EndDate >= DateTime.Now));
                    var organigrams = organigramResponse.Result.Where(a => a.CompanyDepartamentId == companyDeparmentId);
                    var list = new List<OrganigramDto>(); //helper to search better
                    // add the company
                    var company = new OrganigramDto
                    {
                        Id = Guid.Empty,
                        Name = companySiteResponse.Result.Company.Name,
                        Title = "Company",
                        Children = new List<OrganigramDto>()
                    };

                    var ceoData = organigramResponse.Result.FirstOrDefault(a => a.CompanyDepartamentId == null && a.IsCeo);


                    var companyCeo = new OrganigramDto() { Id = ceoData.Id, Name = "", Title = ceoData.Name, Children = new List<OrganigramDto>() };

                    var ceoDt = employeeResponse.Result.FirstOrDefault(a => a.OrganigramId == companyCeo.Id);
                    if (ceoDt != null)
                    {
                        companyCeo.Name = string.Join(ceoDt.Employee.Name, "", ceoDt.Employee.LastName);
                    }
                    company.Children.Add(companyCeo);



                    var items = (from org in organigrams
                                 join emp in employees on org.Id equals emp.OrganigramId into ps
                                 from dt in ps.DefaultIfEmpty()
                                 select new
                                 {
                                     org.Id,
                                     Name = dt?.Employee == null ? "" : string.Join(dt.Employee.Name, " : ", dt.Employee.LastName),
                                     Title = org.Name,
                                     RespondsTo = org.RespondsToId,
                                     org.IsCeo,
                                     org.CompanyDepartamentId,
                                     org.CompanyDepartament
                                 }).ToList();


                    // add the dep ceo-site ceo
                 
                    
                    var siteCeo = new OrganigramDto() { Children = new List<OrganigramDto>() };
                    companyCeo.Children.Add(siteCeo);

                    var siteCeoData = items.FirstOrDefault(a => a.IsCeo);
                    if (siteCeoData is null)
                    {
                        siteCeo.Id = Guid.Empty;
                        siteCeo.Name = companySiteResponse.Result.Departament.Name+ " ceo";
                        siteCeo.Title = companySiteResponse.Result.Departament.Name + " ceo";


                    }
                    else
                    {
                        siteCeo.Id = siteCeoData.Id;
                        siteCeo.Name = siteCeoData.Name;
                        siteCeo.Title = siteCeoData.Title;
                        
                    }

                    //add the children TDDDD
                    var retryList = new List<OrganigramDto>();

                    foreach (var item in items.Where(a => !a.IsCeo && a.RespondsTo != null))
                    {
                        var father = list.FirstOrDefault(a => a.Id == item.RespondsTo);
                        var dt = new OrganigramDto
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Title = item.Title,
                            Children = new List<OrganigramDto>(),
                            ParentId = item.RespondsTo ?? Guid.Empty
                        };
                        if (father != null)
                        {

                            father.Children.Add(dt);
                            list.Add(dt);
                        }
                        else
                        {
                            if (items.Any(a => a.Id == item.RespondsTo))// if we have a father
                            {
                                retryList.Add(dt);
                            }
                        }

                    }
                    // in case we need to 
                    while (retryList.Count > 0)
                    {
                        var tmplist = retryList.ToList();
                        foreach (var ch in tmplist)
                        {
                            var father = list.FirstOrDefault(a => a.Id == ch.ParentId);

                            if (father != null)
                            {
                                list.Add(ch);// to keep the search simplier
                                father.Children.Add(ch);
                                retryList.Remove(ch);
                            }
                        }
                    }
                    list.Clear();
                    retryList.Clear();

                    result.Items = new List<OrganigramDto> { company };
                }
            }
            else
            {
                result.ErrorMessage = companySiteResponse.Message;
            }

            return Json(result);
        }

        #region Helpers
        private SelectDataDTO Parse(CompanyDepartament a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = string.Concat(a.Company.Name, ":", a.Departament.Name)
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

        private SelectDataDTO Parse(Departament a)
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

        private SelectDataDTO Parse(PayrollSeason source)
        {
            return new SelectDataDTO
            {
                Id = source.Id.ToString(),
                Text = source.Name
            };
        }
        #endregion

    }
}