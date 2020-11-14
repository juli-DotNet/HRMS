using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class RegionService : BaseService, IRegionService
    {
        private readonly IUniOfWork work;

        public RegionService(IUniOfWork work)
        {
            this.work = work;
        }
        async Task<bool> DoesRegionExistAsync(string code, int? id)
        {
            var result = id.HasValue ? await work.Region.AnyAsync(a => a.Name.ToLower() == code.ToLower() && a.IsValid && a.Id != id) :
                await work.Region.AnyAsync(a => a.Name.ToLower() == code.ToLower() && a.IsValid);
            return result;
        }

        public async Task<Response<int>> CreateAsync(Region model)
        {
            var result = new Response<int>() { IsSuccessful = true };
            try
            {
                if (await DoesRegionExistAsync(model.Name, null))
                {
                    throw new HRMSException("Region already exists");
                }

                if (model.CountryId == 0)
                {
                    throw new HRMSException("Country cant be emty");
                }
                await work.Region.InsertAsync(model);
                await work.SaveChangesAsync();

                var region = work.Region.Where(a => a.Name == model.Name && a.IsValid).FirstOrDefault();

                if (region == null)
                {
                    throw new HRMSException("Country wasnt saved correctly");
                }
                result.Result = region.Id;
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response> EditAsync(Region model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                if (await DoesRegionExistAsync(model.Name, model.Id))
                {
                    throw new HRMSException("Region already exists");
                }

                if (model.CountryId == 0)
                {
                    throw new HRMSException("Country cant be emty");
                }
                var currentEntity = await work.Region.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("region cant be saved");
                }
                currentEntity.Name = model.Name;
                currentEntity.CountryId = model.CountryId;

                await work.Region.UpdateAsync(currentEntity);
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
                var region = await work.Region.GetByIdAsync(id);
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

        public async Task<Response<Region>> GetByIdAsync(int id)
        {
            var result = new Response<Region> { IsSuccessful = true };
            try
            {
                result.Result = await work.Region.GetByIdAsync(id);

                result.Result.Country = await work.Country.GetByIdAsync(result.Result.CountryId);

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Region>>> GetAllAsync(int? counryId)
        {
            var result = new Response<IEnumerable<Region>> { IsSuccessful = true };
            try
            {
                if (counryId.HasValue && counryId.Value > 0)
                {
                    result.Result = await work.Region.WhereAsync(a => a.IsValid && a.CountryId == counryId);
                }
                else
                {
                    result.Result = await work.Region.WhereAsync(a => a.IsValid);
                }

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
