using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace HRMS.Core.Services
{
    public class RegionService : BaseService, IRegionService
    {
        private readonly IUniOfWork work;

        public RegionService(IUniOfWork work)
        {
            this.work = work;
        }
        public Response<int> Create(Region model)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response Edit(Region model)
        {
            throw new NotImplementedException();
        }

        public Response<IEnumerable<Region>> GetAll(int? counryId)
        {
            var result = new Response<IEnumerable<Region>>();
            //try
            //{
            //    result.Result = counryId.HasValue ?work.Region.Where(a=>a.IsValid&&a.CountryId==counryId) :work.Country.GetAll();
            //    result.IsSuccessful = true;
            //}
            //catch (Exception ex)
            //{
            //    result.Exception = ex;
            //    result.IsSuccessful = false;
            //}
            return result;
        }

        public Response<Region> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
