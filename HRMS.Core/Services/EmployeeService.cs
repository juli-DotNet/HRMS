using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IUniOfWork work;

        public EmployeeService(IUniOfWork work)
        {
            this.work = work;
        }
        private Task<bool> DoesEmployeeExistAsync(string name, string lastName, DateTime bithDate, Guid? p)
        {
            throw new NotImplementedException();
        }
        public async Task<Response> CreateAsync(Employee model)
        {
            var result = new Response() { IsSuccessful = true };
            try
            {
                if (await DoesEmployeeExistAsync(model.Name,model.LastName,model.BithDate, null))
                {
                    throw new HRMSException("Employee already exists");
                }
                await work.Employee.InsertAsync(model);
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
                var currentEntity = await work.Employee.GetByIdAsync(id);
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

        public async Task<Response> EditAsync(Employee model)
        {
            var result = new Response() { IsSuccessful = true };
            try
            {
                if (await DoesEmployeeExistAsync(model.Name, model.LastName, model.BithDate, model.Id))
                {
                    throw new HRMSException("Employee already exists");
                }

                var currentEntity = await work.Employee.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Employee cant be saved,entity couldnt be found");
                }
                currentEntity.Name = model.Name;
                currentEntity.LastName = model.LastName;
                currentEntity.Email = model.Email;
                currentEntity.BithDate = model.BithDate;
                currentEntity.Telephone = model.Telephone;
                currentEntity.Mobile = model.Mobile;
                currentEntity.ContactId = model.ContactId;
                currentEntity.AddressId = model.AddressId;

                await work.Employee.UpdateAsync(currentEntity);
                await work.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<Employee>>> GetAllAsync(Guid? companyId)
        {
            var result = new Response<IEnumerable<Employee>> { IsSuccessful = true };
            try
            {
                if (companyId.HasValue)
                {
                    //TD
                    result.Result = await work.Employee.WhereAsync(a => a.IsValid);
                }
                else
                {
                    result.Result = await work.Employee.WhereAsync(a => a.IsValid);
                }

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<Employee>> GetByIdAsync(Guid id)
        {
            var result = new Response<Employee> { IsSuccessful = true };
            try
            {
                result.Result = await work.Employee.GetByIdAsync(id);

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
