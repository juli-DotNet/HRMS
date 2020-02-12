using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class OrganigramService : BaseService, IOrganigramService
    {
        private readonly IUniOfWork work;

        public OrganigramService(IUniOfWork work)
        {
            this.work = work;
        }
        private async Task<bool> DoesOrganigramExistAsync(string name, Guid companySiteId, Guid? id)
        {
            var result = id.HasValue ? await work.Organigram.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.CompanySiteId == companySiteId && a.IsValid && a.Id != id)
               : await work.Organigram.AnyAsync(a => a.Name.ToLower() == name.ToLower() && a.CompanySiteId == companySiteId && a.IsValid);
            return result;
        }
        public async Task<Response> CreateAsync(Organigram model)
        {
            var result = new Response() { IsSuccessful = true };
            try
            {
                if (model.CompanySiteId == Guid.Empty || string.IsNullOrEmpty(model.Name))
                {
                    throw new HRMSException("Please enter correct Info(Name,CompanySite)");
                }
                if (await DoesOrganigramExistAsync(model.Name, model.CompanySiteId, null))
                {
                    throw new HRMSException("Site already exists");
                }
                if (model.RespondsToId == Guid.Empty)
                {
                    model.RespondsToId = null;
                }
                await work.Organigram.InsertAsync(model);
                await work.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public async Task<Response> EditAsync(Organigram model)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                if (model.CompanySiteId == Guid.Empty || string.IsNullOrEmpty(model.Name))
                {
                    throw new HRMSException("Please enter correct Info(Name,CompanySite)");
                }
                if (await DoesOrganigramExistAsync(model.Name, model.CompanySiteId, null))
                {
                    throw new HRMSException("Site already exists");
                }
                var currentEntity = await work.Organigram.GetByIdAsync(model.Id);
                if (currentEntity == null)
                {
                    throw new HRMSException("Organigram cant be saved,entity couldnt be found");
                }
                currentEntity.Name = model.Name;
                currentEntity.IsCeo = model.IsCeo;
                currentEntity.CompanySiteId = model.CompanySiteId;
                if (model.RespondsToId != Guid.Empty)
                {
                    currentEntity.RespondsToId = model.RespondsToId;
                }
                else
                {
                    model.RespondsToId = null;
                }

                await work.Organigram.UpdateAsync(currentEntity);
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
                var currentEntity = await work.Organigram.GetByIdAsync(id);
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



        public async Task<Response<IEnumerable<Organigram>>> GetAllAsync(Guid? companyId)
        {
            var result = new Response<IEnumerable<Organigram>> { IsSuccessful = true };
            try
            {
                result.Result = await work.Organigram.WhereAsync(a => a.IsValid, a => a.RespondsTo, a => a.CompanySite);

            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }

        public async Task<Response<Organigram>> GetByIdAsync(Guid id)
        {
            var result = new Response<Organigram> { IsSuccessful = true };
            try
            {
                result.Result = await work.Organigram.FirstOrDefault(a => a.Id == id,
                    a => a.RespondsTo, a => a.CompanySite);

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
