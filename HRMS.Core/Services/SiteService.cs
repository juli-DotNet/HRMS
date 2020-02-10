using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class SiteService : BaseService, ISiteService
    {
        private readonly IUniOfWork work;
        private readonly IAddressService address;

        public SiteService(IUniOfWork work, IAddressService address)
        {
            this.work = work;
            this.address = address;
        }

        async Task<bool> DoesSiteExistAsync(string name, Guid? id)
        {
            var result = id.HasValue ? await work.Site.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid && a.Id != id)
                : await work.Site.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid);
            return result;

        }

       

        public async Task<Response<Guid>> CreateAsync(Site model)
        {
            var result = new Response<Guid>() { IsSuccessful = true };
            try
            {
                if (model.Address.CountryId == 0 || model.Address.RegionId == 0 || model.Address.CityId == 0)
                {
                    throw new HRMSException("Please enter correct address(City,Region,Country)");
                }
                if (await DoesSiteExistAsync(model.Name, null))
                {
                    throw new HRMSException("Site already exists");
                }

                var addressResult = await address.InsertOrUpdate(model.Address);
                
                model.Address = addressResult;
                model.AddressId = addressResult.Id;
                await work.Site.InsertAsync(model);
                await work.SaveChangesAsync();

                var _model = work.Site.Where(a => a.Name == model.Name && a.IsValid).FirstOrDefault();

                if (_model == null)
                {
                    throw new HRMSException("Site wasnt saved correctly");
                }
                result.Result = _model.Id;
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                var currentEntity = await work.Site.GetByIdAsync(id);
                currentEntity.IsValid = false;
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response> EditAsync(Site model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                if (model.Address.CountryId == 0 || model.Address.RegionId == 0 || model.Address.CityId == 0)
                {
                    throw new HRMSException("Please enter correct address(City,Region,Country)");
                }
                if (await DoesSiteExistAsync(model.Name, model.Id))
                {
                    throw new HRMSException("Site already exists");
                }
                var addressResult = await address.InsertOrUpdate(model.Address);

                var currentEntity = await work.Site.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Site cant be saved,entity couldnt be found");
                }
                currentEntity.Address = addressResult;
                currentEntity.Name = model.Name;
                currentEntity.AddressId = addressResult.Id;

                await work.Site.UpdateAsync(currentEntity);
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Site>>> GetAllAsync()
        {
            var result = new Response<IEnumerable<Site>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Site.WhereAsync(a => a.IsValid, a => a.Address, a => a.Address.Country, a => a.Address.Region, a => a.Address.City);

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<Site>> GetByIdAsync(Guid id)
        {
            var result = new Response<Site> { IsSuccessful = true };
            try
            {
                result.Result = await work.Site.FirstOrDefault(a => a.Id == id,
                    a => a.Address, a => a.Address.Country, a => a.Address.Region, a => a.Address.City);

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
