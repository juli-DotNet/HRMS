using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IPayrollSeasonService
    {

        Task<Response> CreateAsync(PayrollSeason model);
        Task<Response> DeleteAsync(int id);
        Task<Response> EditAsync(PayrollSeason model);
        Task<Response<IEnumerable<PayrollSeason>>> GetAllAsync();
        Task<Response<PayrollSeason>> GetByIdAsync(int id);
    }
}
