using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class PayrollService : BaseService, IPayrollService
    {
        private readonly IUniOfWork work;

        public PayrollService(IUniOfWork work)
        {
            this.work = work;
        }
        public async Task<Response> DeleteCompanyPayrollAsync(Guid id)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                var currentEntity = await work.CompanyPayroll.FirstOrDefault(a => a.Id == id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Entity couldnt be found");
                }
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

        public async Task<Response> GenerateAsync(Guid companyId, int segmentId)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                var companyPayroll = await work.CompanyPayroll.FirstOrDefault(a => a.Company.Id == companyId && a.PayrollSegment.Id == segmentId && a.IsValid);

                if (companyPayroll is null)
                {
                    companyPayroll = new CompanyPayroll
                    {
                        IsValid = true,
                        CompanyId = companyId,
                        PayrollSegmentId = segmentId,
                        TotalAmounBruto = 0,
                        TotalAmounNeto = 0,
                        Id = Guid.NewGuid()
                    };
                    await work.CompanyPayroll.InsertAsync(companyPayroll);
                }
                else
                {
                    throw new HRMSException("payroll already generated");
                }

                var segmentData = await work.PayrollSegment.FirstOrDefault(a => a.Id == segmentId && a.IsValid, a => a.PayrollSeason);

                if (segmentData is null || segmentData.PayrollSeason is null)
                {
                    throw new HRMSException("Segment entity couldnt be found");
                }

                var fromdate = new DateTime(segmentData.PayrollSeason.year, segmentData.Nr, 1);
                var realFromDate = fromdate.AddDays(-1);
                var todate = fromdate.AddMonths(1);
                // here we are supposed that we dont have holidays

                var companyOrganigram = await work.Organigram.WhereAsync(a => a.CompanyId == companyId && a.IsValid);
                var ids = companyOrganigram.Select(a => a.Id);
                // get all the employess for these dates.

                var employes = await work.OrganigramEmployee.WhereAsync(a => ids.Contains(a.OrganigramId) && a.StartDate < realFromDate);

                decimal sumAmount = 0, sumNeto = 0, tmp = 0;
                foreach (var employee in employes)
                {
                    //not realy correct
                    if (!employee.EndDate.HasValue || employee.EndDate > todate)
                    {
                        await work.EmployeeCompanyPayroll.InsertAsync(new EmployeeCompanyPayroll
                        {
                            Id=Guid.NewGuid(),
                            CompanyPayrollId = companyPayroll.Id,
                            EmployeeId = employee.EmployeeId,
                            IsValid = true,
                            NetoAmount = employee.NetAmountInMonth,
                            BrutoAmount = employee.BrutoAmountInMonth,
                            OrganigramEmployeeId = employee.Id
                        });
                        sumAmount += employee.BrutoAmountInMonth;
                        sumNeto += employee.NetAmountInMonth;
                    }
                    else if (!employee.EndDate.HasValue && employee.EndDate > realFromDate)
                    {
                        var days = employee.EndDate.Value.Day - realFromDate.Day;
                        var totalDays = todate.Day - realFromDate.Day;

                        tmp = days / totalDays;

                        await work.EmployeeCompanyPayroll.InsertAsync(new EmployeeCompanyPayroll
                        {
                            Id=Guid.NewGuid(),
                            CompanyPayrollId = companyPayroll.Id,
                            EmployeeId = employee.EmployeeId,
                            IsValid = true,
                            NetoAmount = employee.NetAmountInMonth * tmp,
                            BrutoAmount = employee.BrutoAmountInMonth * tmp,
                            OrganigramEmployeeId = employee.Id
                        });
                        sumAmount += employee.BrutoAmountInMonth * tmp;
                        sumNeto += employee.NetAmountInMonth * tmp;
                    }

                }
                companyPayroll.TotalAmounBruto = sumAmount;
                companyPayroll.TotalAmounNeto = sumNeto;

                await work.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<CompanyPayroll>> GetByIdAsync(Guid id)
        {
            var result = new Response<CompanyPayroll> { IsSuccessful = true };
            try
            {
                var currentEntity = await work.CompanyPayroll.WhereAsync(a => a.Id == id,a=>a.Company,a=>a.PayrollSegment,a=>a.PayrollSegment.PayrollSeason);
                if (currentEntity == null)
                {
                    throw new HRMSException("Entity couldnt be found");
                }
                result.Result = currentEntity.FirstOrDefault();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<CompanyPayroll>>> GetGeneratedPayrolls(Guid companyId)
        {
            var result = new Response<IEnumerable<CompanyPayroll>> { IsSuccessful = true };
            try
            {
                var currentEntitys = await work.CompanyPayroll.WhereAsync(a => a.CompanyId == companyId && a.IsValid,a=>a.Company,a=>a.PayrollSegment,a=>a.PayrollSegment.PayrollSeason);
                result.Result = currentEntitys;
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<IEnumerable<EmployeeCompanyPayroll>>> GetPayrollEmployees(Guid id)
        {
            var result = new Response<IEnumerable<EmployeeCompanyPayroll>> { IsSuccessful = true };
            try
            {
                var currentEntitys = await work.EmployeeCompanyPayroll.WhereAsync(a => a.CompanyPayrollId==id, a => a.Employee, a => a.OrganigramEmployee, a => a.OrganigramEmployee.Organigram);
                result.Result = currentEntitys;
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response> PayCompanyPayrollAsync(Guid id, bool isPayed)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                var currentEntity = await work.CompanyPayroll.FirstOrDefault(a => a.Id == id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Entity couldnt be found");
                }
                currentEntity.IsPayed = isPayed;
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
