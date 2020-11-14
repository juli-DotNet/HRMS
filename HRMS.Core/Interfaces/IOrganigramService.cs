using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IOrganigramService
    {
        Task<Response> CreateAsync(Organigram model);
        Task<Response> DeleteAsync(Guid id);
        Task<Response> EditAsync(Organigram model);
        Task<Response<IEnumerable<Organigram>>> GetAllAsync(Guid? companyId);
        Task<Response<Organigram>> GetByIdAsync(Guid id);

        Task<Response<OrganigramEmployee>> GetCurrentEmployeeDetailsForOrganigram(Guid organigramId,DateTime? currentDate);
        Task<Response<IEnumerable<OrganigramEmployee>>> GetOrganigramEmployeHistory(Guid organigramId);
        Task<Response> AddEmployee(OrganigramEmployee model);
        Task<Response> EditEmployee(OrganigramEmployee model);
        Task<Response> DeleteEmployeeDetail(Guid id);
        Task<Response<OrganigramEmployee>> GetCurrentEmployeeDetails(Guid id);
    }
}
