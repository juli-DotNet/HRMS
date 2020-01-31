using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly IUniOfWork work;

        public CountryService(IUniOfWork work)
        {
            this.work = work;
        }


        async Task<bool> DoesCountryExistAsync(string code)
        {
            var result = await work.Country.AnyAsync(a => a.Code.ToLower() == code.ToLower() && a.IsValid);
            return result;
        }

        public async Task<Response<int>> CreateAsync(Country model)
        {
            var result = new Response<int>() { IsSuccessful = true };
            try
            {
                if (await DoesCountryExistAsync(model.Code))
                {
                    throw new Exception("Country already exists");
                }

                await work.Country.InsertAsync(model);
                await work.SaveChangesAsync();

                var country = work.Country.Where(a => a.Code == model.Code && a.IsValid).FirstOrDefault();

                if (country == null)
                {
                    throw new Exception("Country wasnt saved correctly");
                }
                result.Result = country.Id;
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response> EditAsync(Country model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                if (await DoesCountryExistAsync(model.Code))
                {
                    throw new Exception("Country already exists");
                }
                await work.Country.UpdateAsync(model);
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response> DeleteAsync(int id)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                var country = await work.Country.GetByIdAsync(id);
                country.IsValid = false;
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Country>>> GetAllAsync()
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

        public async Task<Response<Country>> GetByIdAsync(int id)
        {
            var result = new Response<Country> { IsSuccessful = true };
            try
            {
                result.Result = await work.Country.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
    }
}
