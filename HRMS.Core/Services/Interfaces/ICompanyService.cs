using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Response<int>> CreateAsync(Company model);
        Task<Response> DeleteAsync(Guid id);
        Task<Response> EditAsync(Company model);
        Task<Response<IEnumerable<Company>>> GetAllAsync();
        Task<Response<Company>> GetByIdAsync(Guid id);
    }
}
