using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Response<Guid>> CreateAsync(Company model);
        Task<Response<Guid>> LinkSiteAsync(CompanySite model);
        Task<Response<CompanySite>> GetLinkedSites(CompanySite model);
        Task<Response<CompanySite>> RemoveLinkedSite(Guid companyId,Guid siteId);
        Task<Response> DeleteAsync(Guid id);
        Task<Response> EditAsync(Company model);
        Task<Response<IEnumerable<Company>>> GetAllAsync();
        Task<Response<Company>> GetByIdAsync(Guid id);
    }
}
