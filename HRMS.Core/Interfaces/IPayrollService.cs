using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IPayrollService
    {
        Task<Response> GenerateAsync(Guid companyId, int segmentId);
        Task<Response> PayCompanyPayrollAsync(Guid guid, bool isPayed);
        Task<Response> DeleteCompanyPayrollAsync(Guid guid);
        Task<Response<IEnumerable<CompanyPayroll>>> GetGeneratedPayrolls(Guid companyId);
        Task<Response<CompanyPayroll>> GetByIdAsync(Guid id);
        Task<Response<IEnumerable<EmployeeCompanyPayroll>>> GetPayrollEmployees(Guid id);
    }
}
