using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IJsonService
    {
        Task<Response<IEnumerable<Country>>> GetAllCountriesAsync(string term);
        Task<Response<IEnumerable<Region>>> GetAllRegionsAsync(string term,int? countryId);
        Task<Response<IEnumerable<City>>> GetAllCitiesAsync(string term,int? countryId,int? regionId);
        Task<Response<IEnumerable<Departament>>> GetAllDepartmentsAsync(string search,Guid? company);
        Task<Response<IEnumerable<CompanyDepartament>>> GetCompanyDepartmentsAsync(string search,Guid? company);
        Task<Response<IEnumerable<Employee>>> GetAllContactsAsync(string search);
        Task<Response<IEnumerable<PayrollSeason>>> GetPayrollSeasonsAsync(string search);
        Task<Response<IEnumerable<Organigram>>> GetOrganigramsAsync(string search,Guid? companyDepartmentId);

        Task<Response<IEnumerable<Organigram>>> GetCompanyOrganigramsAsync(Guid companyDepartmentId);
        Task<Response<IEnumerable<OrganigramEmployee>>> GetCompanyEmployesAsync(Guid companyId);
    }
}
