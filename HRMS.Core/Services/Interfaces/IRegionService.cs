using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;

namespace HRMS.Core.Services.Interfaces
{
    public interface IRegionService
    {
        Response<int> Create(Region model);
        Response Delete(int id);
        Response Edit(Region model);
        Response<IEnumerable<Region>> GetAll( int? counryId);
        Response<Region> GetById(int id);
    }
}
