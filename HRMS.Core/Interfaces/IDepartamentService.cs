using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IDepartamentService
    {
        Task<Response> CreateAsync(Departament model);
        Task<Response> DeleteAsync(Guid id);
        Task<Response> EditAsync(Departament model);
        Task<Response<IEnumerable<Departament>>> GetAllAsync();
        Task<Response<Departament>> GetByIdAsync(Guid id);
    }
}
