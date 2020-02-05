using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IUniOfWork work;

        public CompanyService(IUniOfWork work)
        {
            this.work = work;
        }

        async Task<bool> DoesCompanyExistAsync(string name, Guid? id)
        {
            var result = id.HasValue ? await work.Company.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid && a.Id != id)
                : await work.Company.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid);
            return result;
        }
        public async Task<Response<Guid>> CreateAsync(Company model)
        {
            var result = new Response<Guid>() { IsSuccessful = true };
            try
            {
                if (await DoesCompanyExistAsync(model.Name, null))
                {
                    throw new HRMSException("Company already exists");
                }
                if (await DoesCompanyExistAsync(model.NIPT, null))
                {
                    throw new HRMSException("Company already exists-Nipt");
                }

                await work.Company.InsertAsync(model);
                await work.SaveChangesAsync();

                var _model = work.Company.Where(a => a.Name == model.Name && a.IsValid).FirstOrDefault();

                if (_model == null)
                {
                    throw new HRMSException("Company wasnt saved correctly");
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
                if (await DoesCompanyExistAsync(model.Name, model.Id))
                {
                    throw new HRMSException("Company already exists");
                }
                if (await DoesCompanyExistAsync(model.NIPT, model.Id))
                {
                    throw new HRMSException("Company already exists-Nipt");
                }
                var currentEntity = await work.Company.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Company cant be saved,entity couldnt be found");
                }
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
                result.Result = await work.Company.GetAllAsync();

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
                result.Result = await work.Company.GetByIdAsync(id);

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
