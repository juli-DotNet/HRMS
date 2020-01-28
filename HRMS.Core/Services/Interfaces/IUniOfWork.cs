using HRMS.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Core.Services.Interfaces
{
    public interface IUniOfWork
    {
        IRepository<Address> Address { get; }
        IRepository<City> City { get;  }
        IRepository<Company> Company { get;}
        IRepository<CompanySite> CompanySite { get; }
        IRepository<Country> Country { get; }
        IRepository<Employee> Employee { get;}
        IRepository<Organigram> Organigram { get;}
        IRepository<OrganigramEmployee> OrganigramEmployee { get; }
        IRepository<Region> Region { get;  }
        IRepository<Site> Site { get;  }

        void SaveChanges();
    }

}
