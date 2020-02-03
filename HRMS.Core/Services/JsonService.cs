using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class JsonService : BaseService, IJsonService
    {
        private readonly IUniOfWork work;

        public JsonService(IUniOfWork work)
        {
            this.work = work;
        }

       

        public async Task<Response<IEnumerable<Country>>> GetAllCountriesAsync()
        {
            var result = new Response<IEnumerable<Country>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Country.WhereAsync(a => a.IsValid);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public Task<Response<IEnumerable<City>>> GetAllCitiesAsync(int? countryId, int? regionId)
        {
            throw new NotImplementedException();
        }
        public Task<Response<IEnumerable<Region>>> GetAllRegionsAsync(int? countryId)
        {
            throw new NotImplementedException();
        }
    }
}
