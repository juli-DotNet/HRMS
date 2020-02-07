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

        public SiteService(IUniOfWork work)
        {
            this.work = work;
        }

        async Task<bool> DoesSiteExistAsync(string name, Guid? id)
        {
            var result = id.HasValue ? await work.Site.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid && a.Id != id)
                : await work.Site.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid);
            return result;

        }

        async Task<Guid> GetAddressIdAsync(Address address)
        {
            var result = (await work.Address.WhereAsync(
                        a =>
                            a.StreetName.ToLower() == address.StreetName.ToLower() &&
                            a.IsValid &&
                            a.CityId == address.CityId
                            )).FirstOrDefault();
            return result?.Id ?? Guid.Empty;

        }

        public async Task<Response<Guid>> CreateAsync(Site model)
        {
            var result = new Response<Guid>() { IsSuccessful = true };
            try
            {
                if (await DoesSiteExistAsync(model.Name, null))
                {
                    throw new HRMSException("Site already exists");
                }

                var addressId = await GetAddressIdAsync(model.Address);

                if (addressId != Guid.Empty)
                {
                    model.AddressId = addressId;
                    model.Address = null;
                }

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
                if (await DoesSiteExistAsync(model.Name, model.Id))
                {
                    throw new HRMSException("Site already exists");
                }
                var addressId = await GetAddressIdAsync(model.Address);

                if (addressId != Guid.Empty)
                {
                    model.AddressId = addressId;
                    model.Address = null;
                }

                var currentEntity = await work.Site.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Site cant be saved,entity couldnt be found");
                }
                currentEntity.Name = model.Name;


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
                result.Result = (await work.Site.GetAllAsync()).Where(a => a.IsValid);

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
                result.Result = await work.Site.GetByIdAsync(id);

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
