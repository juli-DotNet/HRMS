using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class DepartmentService : BaseService, IDepartamentService
    {
        private readonly IUniOfWork work;
        private readonly IAddressService address;

        public DepartmentService(IUniOfWork work, IAddressService address)
        {
            this.work = work;
            this.address = address;
        }

        async Task<bool> DoesSiteExistAsync(string name, Guid? id)
        {
            var result = id.HasValue ? await work.Departament.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid && a.Id != id)
                : await work.Departament.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.IsValid);
            return result;

        }


        private async Task IsModelValid(Departament model, bool checkId = false)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new HRMSException("Name is a required field");
            }
            if (checkId)
            {
                if (await DoesSiteExistAsync(model.Name, model.Id))
                {
                    throw new HRMSException("Department already exists");
                }
            }
            else
            {
                if (await DoesSiteExistAsync(model.Name, null))
                {
                    throw new HRMSException("Department already exists");
                }
            }
        }
        public async Task<Response<IEnumerable<Departament>>> GetAllAsync()
        {
            var result = new Response<IEnumerable<Departament>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Departament.WhereAsync(a => a.IsValid);

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<Departament>> GetByIdAsync(Guid id)
        {
            var result = new Response<Departament> { IsSuccessful = true };
            try
            {
                result.Result = await work.Departament.FirstOrDefault(a => a.Id == id);

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
                var currentEntity = await work.Departament.GetByIdAsync(id);
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

        public async Task<Response> CreateAsync(Departament model)
        {
            var result = new Response<Guid>() { IsSuccessful = true };
            try
            {

                await IsModelValid(model);

                model.Id = Guid.NewGuid();
                await work.Departament.InsertAsync(model);
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

       

        public async Task<Response> EditAsync(Departament model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                await IsModelValid(model, true);

                var currentEntity = await work.Departament.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Department cant be saved,entity couldnt be found");
                }
                currentEntity.Name = model.Name;

                await work.Departament.UpdateAsync(currentEntity);
                await work.SaveChangesAsync();
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
