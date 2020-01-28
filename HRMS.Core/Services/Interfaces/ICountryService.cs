using HRMS.Core.Model;
using System.Collections.Generic;

namespace HRMS.Core.Services.Interfaces
{
    public interface ICountryService
    {
        int Create(Country model);
        void Delete(int id);
        void Edit(Country model);
        List<Country> GetAll();
        Country GetById(int id);
    }
}
