using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class CompanySiteService : BaseService, ICompanySiteService
    {
        private readonly IUniOfWork work;

        public CompanySiteService(IUniOfWork work)
        {
            this.work = work;
        }

        public async Task<Response<List<CompanySite>>> GetAllAsync(Guid companyId)
        {
            var result = new Response<List<CompanySite>> { IsSuccessful = true };
            try
            {
                result.Result = await work.CompanySite.WhereAsync(a =>
                    a.CompanyId == companyId && a.IsValid,
                    a => a.Site,
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

        public async Task<Response<CompanySite>> GetById(Guid id)
        {
            var result = new Response<CompanySite> { IsSuccessful = true };
            try
            {
                result.Result = await work.CompanySite.FirstOrDefault(a =>
                    a.Id == id && a.IsValid,
                    a => a.Site,
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
    }
}
