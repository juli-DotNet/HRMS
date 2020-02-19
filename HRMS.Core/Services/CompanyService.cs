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
        async Task<bool> DoesCompanyNiptExistAsync(string name, Guid? id)
        {
            var result = id.HasValue ? await work.Company.AnyAsync(a => a.NIPT.ToLower() == name.ToLower() && a.IsValid && a.Id != id)
                : await work.Company.AnyAsync(a => a.NIPT.ToLower() == name.ToLower() && a.IsValid);
            return result;
        }
        public async Task<Response<Guid>> CreateAsync(Company model)
        {
            var result = new Response<Guid>() { IsSuccessful = true };
            try
            {
                model.Id = Guid.NewGuid();
                if (await DoesCompanyExistAsync(model.Name, null))
                {
                    throw new HRMSException("Company already exists");
                }
                if (await DoesCompanyNiptExistAsync(model.NIPT, null))
                {
                    throw new HRMSException("Company already exists-Nipt");
                }
                //Add Company Ceo
               await work.Organigram.InsertAsync(new Organigram {
                    Id=Guid.Empty,
                    CompanyId=model.Id,
                    CompanySiteId=null,
                    IsCeo=true,
                    Name=model.Name+" Ceo",
                    IsValid=true,
                    Company=model
                    
                });

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
                if (await DoesCompanyNiptExistAsync(model.NIPT, model.Id))
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
                result.Result = (await work.Company.GetAllAsync()).Where(a => a.IsValid);

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

        public async Task<Response<List<CompanySite>>> GetLinkedSites(Guid companyId)
        {
            var result = new Response<List<CompanySite>> { IsSuccessful = true };
            try
            {
                result.Result = await work.CompanySite.WhereAsync(a =>
                    a.CompanyId == companyId && a.IsValid,
                    a => a.Site,
                    a => a.Site.Address,
                    a => a.Site.Address.City,
                    a => a.Site.Address.Country,
                    a => a.Site.Address.Region
                    );

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response> LinkSiteAsync(Guid companyId, Guid siteId)
        {
            var result = new Response { IsSuccessful = true };
            try
            {

                var companySites = await work.CompanySite.WhereAsync(a => a.CompanyId == companyId && a.SiteId == siteId);

                if (companySites == null || companySites.Count == 0)
                {
                    await work.CompanySite.InsertAsync(new CompanySite
                    {
                        CompanyId = companyId,
                        SiteId = siteId
                    });
                }
                else
                {
                    var lastCompanySite = companySites.OrderByDescending(a => a.CreatedOn).First();
                    lastCompanySite.IsValid = true;
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


        public async Task<Response> RemoveLinkedSite(Guid id)
        {
            var result = new Response { IsSuccessful = true };
            try
            {

                var companySite = await work.CompanySite.FirstOrDefault(a => a.Id == id && a.IsValid);

                if (companySite == null)
                {
                    throw new HRMSException("entity couldnt be found");
                }
                else
                {
                    companySite.IsValid = false;
                    companySite.ModifiedOn = DateTime.Now;
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
