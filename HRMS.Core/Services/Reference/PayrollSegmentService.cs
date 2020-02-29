using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class PayrollSegmentService : BaseService, IPayrollSegmentService
    {
        private readonly IUniOfWork work;

        public PayrollSegmentService(IUniOfWork work)
        {
            this.work = work;
        }

        async Task IsModelValid(PayrollSegment season, bool checkId)
        {

            if (season.Nr == 0)
            {
                throw new Exception("Nr is required");
            }
            if (season.PayrollSeasonId == 0)
            {
                throw new Exception("Payroll season is required");
            }
            if (checkId)
            {
                if (await work.PayrollSegment.AnyAsync(a => a.Nr == season.Nr && a.PayrollSeasonId == season.PayrollSeasonId && a.IsValid && a.Id != season.Id))
                {
                    throw new Exception("Nr is not unique");
                }
            }
            else
            {
                if (await work.PayrollSegment.AnyAsync(a => a.Nr == season.Nr && a.PayrollSeasonId == season.PayrollSeasonId && a.IsValid))
                {
                    throw new Exception("Nr is not unique");
                }
            }

        }
        public async Task<Response> CreateAsync(PayrollSegment model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                await IsModelValid(model, false);
                await work.PayrollSegment.InsertAsync(model);
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
                var region = await work.PayrollSegment.GetByIdAsync(id);
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

        public async Task<Response> EditAsync(PayrollSegment model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                await IsModelValid(model, true);

                var currentEnity = await work.PayrollSegment.FirstOrDefault(a => a.Id == model.Id && a.IsValid);

                if (currentEnity is null)
                {
                    throw new Exception("Entity couldn't be found");
                }
                currentEnity.Nr = model.Nr;
                currentEnity.Name = model.Name;
                currentEnity.PayrollSeasonId = model.PayrollSeasonId;

                await work.PayrollSegment.UpdateAsync(currentEnity);
                await work.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<PayrollSegment>>> GetAllAsync(int? seasonId)
        {
            var result = new Response<IEnumerable<PayrollSegment>> { IsSuccessful = true };
            try
            {
                if (seasonId.HasValue)
                    result.Result = await work.PayrollSegment.WhereAsync(a => a.IsValid && a.PayrollSeasonId == seasonId, a => a.PayrollSeason);
                else
                    result.Result = await work.PayrollSegment.WhereAsync(a => a.IsValid, a => a.PayrollSeason);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<PayrollSegment>> GetByIdAsync(int id)
        {
            var result = new Response<PayrollSegment> { IsSuccessful = true };
            try
            {
                result.Result = await work.PayrollSegment.FirstOrDefault(a => a.IsValid && a.Id == id, a => a.PayrollSeason);
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
