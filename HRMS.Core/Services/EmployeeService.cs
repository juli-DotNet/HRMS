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
        private readonly IAddressService address;

        public EmployeeService(IUniOfWork work, IAddressService address)
        {
            this.work = work;
            this.address = address;
        }

        private async Task<bool> DoesEmployeeExistAsync(string name, string lastName, DateTime bithDate, Guid? id)
        {
            var result = id.HasValue ? await work.Employee.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.LastName.ToLower() == lastName.ToLower() && a.IsValid && a.Id != id)
             : await work.Employee.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.LastName.ToLower() == lastName.ToLower() && a.IsValid);
            return result;
        }

        public async Task<Response> CreateAsync(Employee model)
        {
            var result = new Response() { IsSuccessful = true };
            try
            {
                if (await DoesEmployeeExistAsync(model.Name, model.LastName, model.BirthDate, null))
                {
                    throw new HRMSException("Employee already exists");
                }
                if (model.ContactId == Guid.Empty)
                {
                    model.ContactId = null;
                }
                var addressResult = await address.InsertOrUpdate(model.Address);
                model.AddressId = addressResult.Id;
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
                if (await DoesEmployeeExistAsync(model.Name, model.LastName, model.BirthDate, model.Id))
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
                currentEntity.BirthDate = model.BirthDate;
                currentEntity.Telephone = model.Telephone;
                currentEntity.Mobile = model.Mobile;
                if (model.ContactId != Guid.Empty)
                {
                    currentEntity.ContactId = model.ContactId;
                }
                var addressResult = await address.InsertOrUpdate(model.Address);
                currentEntity.AddressId = addressResult.Id;

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

        public async Task<Response<IEnumerable<Employee>>> GetAllAsync()
        {
            var result = new Response<IEnumerable<Employee>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Employee.WhereAsync(a => a.IsValid, a => a.Contact, a => a.Address, a => a.Address.City, a => a.Address.Region, a => a.Address.Country);
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
                result.Result = await work.Employee.FirstOrDefault(a => a.IsValid && a.Id == id, a => a.Contact, a => a.Address, a => a.Address.City, a => a.Address.Region, a => a.Address.Country);

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
