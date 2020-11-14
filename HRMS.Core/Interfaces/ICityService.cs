using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICityService
    {
        Task<Response<int>> CreateAsync(City model);
        Task<Response> DeleteAsync(int id);
        Task<Response> EditAsync(City model);
        Task<Response<IEnumerable<City>>> GetAllAsync(int? counryId,int? regionId);
        Task<Response<City>> GetByIdAsync(int id);
    }
}
