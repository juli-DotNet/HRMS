using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class CityService : BaseService, ICityService
    {
        public Task<Response<int>> CreateAsync(Region model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> EditAsync(Region model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<Region>>> GetAllAsync(int? counryId, int? regionId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Region>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
