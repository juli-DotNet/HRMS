using HRMS.Core.Common;
using HRMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IJsonService
    {
        Task<Response<IEnumerable<Country>>> GetAllCountriesAsync();
        Task<Response<IEnumerable<Region>>> GetAllRegionsAsync(int? countryId);
        Task<Response<IEnumerable<City>>> GetAllCitiesAsync(int? countryId,int? regionId);
    }
}
