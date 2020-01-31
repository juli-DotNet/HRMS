using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace HRMS.Core.Services
{
    public class CityService : BaseService, ICityService
    {
        public Response<int> Create(Region model)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response Edit(Region model)
        {
            throw new NotImplementedException();
        }

        public Response<IEnumerable<Region>> GetAll(int? counryId, int? regionId)
        {
            throw new NotImplementedException();
        }

        public Response<Region> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
