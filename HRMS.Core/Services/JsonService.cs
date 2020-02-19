﻿using HRMS.Core.Common;
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

        public async Task<Response<IEnumerable<Site>>> GetAllSitesAsync(string search, Guid? companyId)
        {
            var result = new Response<IEnumerable<Site>> { IsSuccessful = true };
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    search = "";
                }
                if (companyId.HasValue)
                {
                    result.Result = (await work.CompanySite.WhereAsync(a => a.IsValid && a.Site.Name.ToLower().Contains(search.ToLower()) && a.CompanyId == companyId.Value, a => a.Site))?.Select(a => a.Site);
                }
                else
                {
                    result.Result = (await work.CompanySite.WhereAsync(a => a.IsValid && a.Site.Name.ToLower().Contains(search.ToLower()), a => a.Site))?.Select(a => a.Site);
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

        public async Task<Response<IEnumerable<Organigram>>> GetOrganigramsAsync(string search, Guid? companySiteId)
        {
            var result = new Response<IEnumerable<Organigram>> { IsSuccessful = true };
            try
            {
                if (companySiteId.HasValue)
                {
                    result.Result = await work.Organigram.WhereAsync(a => a.IsValid && a.Name.ToLower().Contains(search.ToLower()) && a.CompanySiteId == companySiteId);
                }
                else
                {
                    result.Result = await work.Organigram.WhereAsync(a => a.IsValid && a.Name.ToLower().Contains(search.ToLower()));
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response<IEnumerable<Organigram>>> GetCompanyOrganigramsAsync(Guid companyId)
        {
            var result = new Response<IEnumerable<Organigram>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Organigram.WhereAsync(a => a.IsValid && a.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response<IEnumerable<OrganigramEmployee>>> GetCompanyEmployesAsync(Guid companyId)
        {
            var result = new Response<IEnumerable<OrganigramEmployee>> { IsSuccessful = true };
            try
            {
                result.Result = await work.OrganigramEmployee.WhereAsync(a => a.IsValid && a.Organigram.CompanyId == companyId, a => a.Employee,a=>a.Organigram);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Organigram>>> GetCompanySiteOrganigramsAsync(Guid id)
        {
            var result = new Response<IEnumerable<Organigram>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Organigram.WhereAsync(a => a.IsValid && a.CompanySiteId == id);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response<IEnumerable<OrganigramEmployee>>> GetCompanySiteEmployesAsync(Guid id)
        {
            var result = new Response<IEnumerable<OrganigramEmployee>> { IsSuccessful = true };
            try
            {
                result.Result = await work.OrganigramEmployee.WhereAsync(a => a.IsValid && a.Organigram.CompanySiteId == id, a => a.Employee,a=>a.Organigram);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response<IEnumerable<CompanySite>>> GetCompanySitesAsync(string search, Guid? companyId)
        {
            var result = new Response<IEnumerable<CompanySite>> { IsSuccessful = true };
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    search = "";
                }
                if (companyId.HasValue)
                {
                    result.Result = (await work.CompanySite.WhereAsync(a => a.IsValid && (a.Site.Name.ToLower().Contains(search.ToLower()) || a.Company.Name.ToLower().Contains(search.ToLower())) && a.CompanyId == companyId.Value, a => a.Site, a => a.Company));
                }
                else
                {
                    result.Result = (await work.CompanySite.WhereAsync(a => a.IsValid && (a.Site.Name.ToLower().Contains(search.ToLower()) || a.Company.Name.ToLower().Contains(search.ToLower())), a => a.Site, a => a.Company));
                }
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
