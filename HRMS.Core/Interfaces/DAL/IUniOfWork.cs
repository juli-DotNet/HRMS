using HRMS.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IUniOfWork
    {
        IRepository<Address> Address { get; }
        IRepository<City> City { get;  }
        IRepository<Company> Company { get;}
        IRepository<CompanyDepartament> CompanyDepartament { get; }
        IRepository<Country> Country { get; }
        IRepository<Employee> Employee { get;}
        IRepository<Organigram> Organigram { get;}
        IRepository<OrganigramEmployee> OrganigramEmployee { get; }
        IRepository<Region> Region { get;  }
        IRepository<Departament> Departament { get;  }
        IRepository<CompanyPayroll> CompanyPayroll { get;  }
        IRepository<EmployeeCompanyPayroll> EmployeeCompanyPayroll { get;  }
        IRepository<PayrollSeason> PayrollSeason { get;  }
        IRepository<PayrollSegment> PayrollSegment { get;  }

        Task SaveChangesAsync();
    }
}
