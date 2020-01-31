using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICityService
    {
        Task<Response<int>> CreateAsync(Region model);
        Task<Response> DeleteAsync(int id);
        Task<Response> EditAsync(Region model);
        Task<Response<IEnumerable<Region>>> GetAllAsync(int? counryId,int? regionId);
        Task<Response<Region>> GetByIdAsync(int id);
    }
}
