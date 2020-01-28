using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICountryService
    {
        Response<int> Create(Country model);
        Response Delete(int id);
        Response Edit(Country model);
        Response<IEnumerable<Country>> GetAll();
        Response<Country> GetById(int id);
    }
}
