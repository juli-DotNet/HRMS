using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Response> CreateAsync(Employee model);
        Task<Response> DeleteAsync(Guid id);
        Task<Response> EditAsync(Employee model);
        Task<Response<IEnumerable<Employee>>> GetAllAsync();
        Task<Response<Employee>> GetByIdAsync(Guid id);
    }
}
