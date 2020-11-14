using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IPayrollSegmentService
    {
        Task<Response> CreateAsync(PayrollSegment model);
        Task<Response> DeleteAsync(int id);
        Task<Response> EditAsync(PayrollSegment model);
        Task<Response<IEnumerable<PayrollSegment>>> GetAllAsync(int? seasonId);
        Task<Response<PayrollSegment>> GetByIdAsync(int id);
    }
}
