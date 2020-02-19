using HRMS.Core.Common;
using HRMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICompanySiteService
    {
        Task<Response<List<CompanySite>>> GetAllAsync(Guid companyId);
        Task<Response<CompanySite>> GetById(Guid id);
    }
}
