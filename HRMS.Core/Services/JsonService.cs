using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class JsonService : BaseService, IJsonService
    {
        private readonly IUniOfWork work;

        public JsonService(IUniOfWork work)
        {
            this.work = work;
        }



        public async Task<Response<IEnumerable<Country>>> GetAllCountriesAsync(string term)
        {
            var result = new Response<IEnumerable<Country>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Country.WhereAsync(a => a.IsValid && a.Name.ToLower().Contains(term.ToLower()));
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response<IEnumerable<Region>>> GetAllRegionsAsync(string term, int? countryId)
        {
            var result = new Response<IEnumerable<Region>> { IsSuccessful = true };
            try
            {
                if (countryId.HasValue)
                {
                    result.Result = await work.Region.WhereAsync(a =>
                                                                 a.CountryId == countryId &&
                                                                 a.IsValid &&
                                                                 a.Name.ToLower().Contains(term.ToLower())
                                                                );
                }
                else
                {
                    result.Result = await work.Region.WhereAsync(a =>
                                                                 a.IsValid &&
                                                                 a.Name.ToLower().Contains(term.ToLower())
                                                               );
                }



            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<City>>> GetAllCitiesAsync(string term, int? countryId, int? regionId)
        {
            var result = new Response<IEnumerable<City>> { IsSuccessful = true };
            try
            {
                if (countryId.HasValue && !regionId.HasValue)
                {
                    result.Result = await work.City.WhereAsync(a =>
                                                                a.CountryId == countryId &&
                                                                a.IsValid &&
                                                                a.Name.ToLower().Contains(term.ToLower())
                                                           );
                }
                else if (regionId.HasValue)
                {
                    result.Result = await work.City.WhereAsync(a =>
                                                                a.RegionId == regionId &&
                                                                a.IsValid &&
                                                                a.Name.ToLower().Contains(term.ToLower())
                                                           );
                }
                else
                {
                    result.Result = await work.City.WhereAsync(a =>
                                                                a.IsValid &&
                                                                a.Name.ToLower().Contains(term.ToLower())
                                                           );
                }

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Site>>> GetAllSitesAsync(string search, Guid companyId)
        {
            var result = new Response<IEnumerable<Site>> { IsSuccessful = true };
            try
            {
                var currentConnectedSite = (await work.CompanySite.WhereAsync(a => a.IsValid && a.CompanyId == companyId))?.Select(a => a.SiteId);

                if (string.IsNullOrEmpty(search))
                {
                    result.Result = await work.Site.WhereAsync(a => a.IsValid &&
                                                                    !currentConnectedSite.Contains(a.Id)
                                                                );
                }
                else
                {
                    result.Result = await work.Site.WhereAsync(a => a.IsValid &&
                                                                    a.Name.ToLower().Contains(search.ToLower()) &&
                                                                    !currentConnectedSite.Contains(a.Id)
                                                                 );
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Employee>>> GetAllContactsAsync(string search)
        {
            var result = new Response<IEnumerable<Employee>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Employee.WhereAsync(a => a.IsValid && (

                a.LastName.ToLower().Contains(search.ToLower()) ||
                a.Name.ToLower().Contains(search.ToLower())
                )
                );
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
    }
}
