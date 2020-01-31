using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly IUniOfWork work;

        public CountryService(IUniOfWork work)
        {
            this.work = work;
        }

        public Response<IEnumerable<Country>> GetAll()
        {
            var result = new Response<IEnumerable<Country>>();
            try
            {
                //result.Result = work.Country.GetAll();
                //result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;
        }
        public Response<Country> GetById(int id)
        {
            var result = new Response<Country>();
            try
            {
                //result.Result = work.Country.GetById(id);
                //result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccessful = false;
            }

            return result;
        }

        public Response<int> Create(Country model)
        {
            var result = new Response<int>();
            try
            {
                //if (DoesCountryExist(model.Code))
                //{
                //    throw new Exception();
                //}
                //model.CreatedOn = DateTime.Now;
                //model.IsValid = true;
                //work.Country.Insert(model);
                //work.SaveChanges();
                //var country = work.Country.Where(a => a.Name == model.Name && a.IsValid).FirstOrDefault();

                //result.Result = country.Id;
                //result.IsSuccessful = true;

            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;

        }

        public Response Edit(Country model)
        {
            var result = new Response();
            try
            {
                //if (DoesCountryExist(model.Code))
                //{
                //    throw new Exception();
                //}
                //model.IsValid = true;
                //model.ModifiedOn = DateTime.Now;

                //work.Country.Update(model);
                //work.SaveChanges();
                //result.IsSuccessful = true;
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;

        }

        public Response Delete(int id)
        {
            var result = new Response { IsSuccessful = true };
            try
            {
                //var country = work.Country.GetById(id);
                //country.IsValid = false;
                //work.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Exception = ex;
                result.IsSuccessful = false;
            }
            return result;

        }

        bool DoesCountryExist(string name)
        {
            //if (work.Country.Any(a => a.Code.ToLower() == name.ToLower() && a.IsValid))
            //{
            //    return true;
            //}
            return false;
        }
    }
}
