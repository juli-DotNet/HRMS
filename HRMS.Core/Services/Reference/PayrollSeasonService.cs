using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class PayrollSeasonService : BaseService, IPayrollSeasonService
    {
        private readonly IUniOfWork work;

        public PayrollSeasonService(IUniOfWork unit)
        {
            this.work = unit;
        }
        async Task IsModelValid(PayrollSeason season, bool checkId)
        {
            if (string.IsNullOrEmpty(season.Name))
            {
                throw new Exception("Name is required");
            }
            if (season.year == 0)
            {
                throw new Exception("Year is required");
            }
            if (checkId)
            {
                if (await work.PayrollSeason.AnyAsync(a => a.year == season.year && a.IsValid && a.Id != season.Id))
                {
                    throw new Exception("Year is not unique");
                }
            }
            else
            {
                if (await work.PayrollSeason.AnyAsync(a => a.year == season.year && a.IsValid))
                {
                    throw new Exception("Year is not unique");
                }
            }

        }

        public async Task<Response> CreateAsync(PayrollSeason model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                await IsModelValid(model, false);
                await work.PayrollSeason.InsertAsync(model);
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
                var region = await work.PayrollSeason.GetByIdAsync(id);
                region.IsValid = false;
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response> EditAsync(PayrollSeason model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                await IsModelValid(model, true);

                var currentEnity = await work.PayrollSeason.FirstOrDefault(a => a.Id == model.Id && a.IsValid);

                if (currentEnity is null)
                {
                    throw new Exception("Entity couldn't be found");
                }
                currentEnity.year = model.year;
                currentEnity.Name = model.Name;

                await work.PayrollSeason.UpdateAsync(currentEnity);
                await work.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<PayrollSeason>>> GetAllAsync()
        {
            var result = new Response<IEnumerable<PayrollSeason>> { IsSuccessful = true };
            try
            {
                result.Result = await work.PayrollSeason.WhereAsync(a => a.IsValid);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<PayrollSeason>> GetByIdAsync(int id)
        {
            var result = new Response<PayrollSeason> { IsSuccessful = true };
            try
            {
                result.Result = await work.PayrollSeason.FirstOrDefault(a => a.IsValid && a.Id == id);
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
