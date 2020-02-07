using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ISiteService
    {
        Task<Response<Guid>> CreateAsync(Site model);
        Task<Response> DeleteAsync(Guid id);
        Task<Response> EditAsync(Site model);
        Task<Response<IEnumerable<Site>>> GetAllAsync();
        Task<Response<Site>> GetByIdAsync(Guid id);
    }
}
