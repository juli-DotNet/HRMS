using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICompanyDepartamentService
    {
        Task<Response<List<CompanyDepartament>>> GetAllAsync(Guid companyId);
        Task<Response<CompanyDepartament>> GetByIdAsync(Guid id);

        Task<Response> DeleteAsync(Guid id);
        Task<Response> CreateAsync(Guid companyId, Guid departmentId);
        //Task<Response<List<CompanyDepartament>>> GetLinkedDepartmentsAsync(Guid companyId);
    }
}
