using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class CompanyDepartmentService : BaseService, ICompanyDepartamentService
    {
        private readonly IUniOfWork work;

        public CompanyDepartmentService(IUniOfWork work)
        {
            this.work = work;
        }

       

        public async Task<Response<List<CompanyDepartament>>> GetAllAsync(Guid companyId)
        {
            var result = new Response<List<CompanyDepartament>> { IsSuccessful = true };
            try
            {
                result.Result = await work.CompanyDepartament.WhereAsync(a =>
                    a.CompanyId == companyId && a.IsValid,
                    a => a.Departament,
                    a => a.Company
                    );

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<CompanyDepartament>> GetByIdAsync(Guid id)
        {
            var result = new Response<CompanyDepartament> { IsSuccessful = true };
            try
            {
                result.Result = await work.CompanyDepartament.FirstOrDefault(a =>
                    a.Id == id && a.IsValid,
                    a => a.Departament,
                    a => a.Company
                    );

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response> CreateAsync(Guid companyId, Guid departmentId)
        {
            var result = new Response { IsSuccessful = true };
            try
            {

                var companyDepartments = await work.CompanyDepartament.WhereAsync(a => a.CompanyId == companyId && a.DepartamentId == departmentId);

                if (companyDepartments == null || companyDepartments.Count == 0)
                {
                    await work.CompanyDepartament.InsertAsync(new CompanyDepartament
                    {
                        CompanyId = companyId,
                        DepartamentId = departmentId
                    });
                }
                else
                {
                    var lastCompanyDepartment = companyDepartments.OrderByDescending(a => a.CreatedOn).First();
                    lastCompanyDepartment.IsValid = true;
                }
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
                var companyDepartment = await work.CompanyDepartament.FirstOrDefault(a => a.Id == id && a.IsValid);

                if (companyDepartment == null)
                {
                    throw new HRMSException("entity couldnt be found");
                }
                else
                {
                    companyDepartment.IsValid = false;
                    companyDepartment.ModifiedOn = DateTime.Now;
                }
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
