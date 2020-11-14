using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICountryService
    {
        Task<Response<int>> CreateAsync(Country model);
        Task<Response> DeleteAsync(int id);
        Task<Response> EditAsync(Country model);
        Task<Response<IEnumerable<Country>>> GetAllAsync();
        Task<Response<Country>> GetByIdAsync(int id);
    }
}
