using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
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

        public List<Country> GetAll()
        {
            return work.Country.GetAll();
        }
        public Country GetById(int id)
        {
            return work.Country.GetById(id);
        }

        public int Create(Country model)
        {
            if (DoesCountryExist(model.Name))
            {
                return 0;//throw 
            }
            work.Country.Insert(model);
            work.SaveChanges();
            var country = work.Country.Where(a => a.Name == model.Name && a.IsValid).FirstOrDefault();
            return country.Id;
        }

        public void Edit(Country model)
        {
            work.Country.Update(model);
            work.SaveChanges();
        }
        public void Delete(int id)
        {
            var country = work.Country.GetById(id);
            country.IsValid = false;
            work.SaveChanges();
        }

        bool DoesCountryExist(string name)
        {
            if (work.Country.Any(a => a.Name.ToLower() == name.ToLower() && a.IsValid))
            {
                return true;
            }
            return false;
        }
    }
}
