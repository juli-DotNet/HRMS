using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IUniOfWork work;
        private readonly IAddressService addressService;
        private readonly IOrganigramService organigramService;

        public CompanyService(IUniOfWork work, IAddressService addressService, IOrganigramService organigramService)
        {
            this.work = work;
            this.addressService = addressService;
            this.organigramService = organigramService;
        }

        async Task<bool> DoesCompanyExistAsync(string name, Guid? id)
        {
            var result = id.HasValue ? await work.Company.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid && a.Id != id)
                : await work.Company.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid);
            return result;

        }
        async Task<bool> DoesCompanyNiptExistAsync(string nipt, Guid? id)
        {
            var result = id.HasValue ? await work.Company.AnyAsync(a => a.NIPT.ToLower() == nipt.ToLower() && a.IsValid && a.Id != id)
                : await work.Company.AnyAsync(a => a.NIPT.ToLower() == nipt.ToLower() && a.IsValid);
            return result;
        }

        async Task IsModelValid(Company model, bool checkId = false)
        {

            if (model.Address.CountryId == 0 || model.Address.RegionId == 0 || model.Address.CityId == 0)
            {
                throw new HRMSException("Please enter correct address(City,Region,Country)");
            }


            if (checkId &&
                (await DoesCompanyExistAsync(model.Name, model.Id) || await DoesCompanyNiptExistAsync(model.NIPT, model.Id))
                )
            {
                throw new HRMSException("Company (Name or nipt) already exists");
            }
            else if (!checkId && (await DoesCompanyExistAsync(model.Name, null) || await DoesCompanyNiptExistAsync(model.NIPT, null)))
            {
                throw new HRMSException("Company (Name or nipt) already exists");
            }
        }

        private async Task CreateReferenceDatas(Company model)
        {
            var addressResult = await addressService.InsertOrUpdate(model.Address);
            model.Address = addressResult;
            model.AddressId = addressResult.Id;
            //Add Company Ceo
            await organigramService.CreateAsync(new Organigram
            {
                Id = Guid.Empty,
                CompanyId = model.Id,
                CompanyDepartamentId = null,
                IsCeo = true,
                Name = model.Name + " Ceo",
                IsValid = true
            });
        }
        public async Task<Response> CreateAsync(Company model)
        {
            var result = new Response<Guid>() { IsSuccessful = true };
            try
            {
                await IsModelValid(model);


                model.Id = Guid.NewGuid();
                await CreateReferenceDatas(model);

                await work.Company.InsertAsync(model);
                await work.SaveChangesAsync();


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
                var currentEntity = await work.Company.GetByIdAsync(id);
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

        public async Task<Response> EditAsync(Company model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                await IsModelValid(model, true);
                var currentEntity = await work.Company.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Company cant be saved,entity couldnt be found");
                }
                var addressResult = await addressService.InsertOrUpdate(model.Address);

                currentEntity.Address = addressResult;
                currentEntity.AddressId = addressResult.Id;
                currentEntity.Name = model.Name;
                currentEntity.Description = model.Description;
                currentEntity.NIPT = model.NIPT;

                await work.Company.UpdateAsync(currentEntity);
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Company>>> GetAllAsync()
        {
            var result = new Response<IEnumerable<Company>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Company.WhereAsync(a => a.IsValid, a => a.Address, a => a.Address.City, a => a.Address.Region, a => a.Address.Country);

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<Company>> GetByIdAsync(Guid id)
        {
            var result = new Response<Company> { IsSuccessful = true };
            try
            {
                result.Result = await work.Company.FirstOrDefault(a => a.Id == id, a => a.Address, a => a.Address.City, a => a.Address.Region, a => a.Address.Country);

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
