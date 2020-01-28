using HRMS.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Core.Services.Interfaces
{
    public interface IHRMSContext
    {
        DbSet<Address> Address { get; set; }
        DbSet<City> City { get; set; }
        DbSet<Company> Company { get; set; }
        DbSet<CompanySite> CompanySite { get; set; }
        DbSet<Country> Country { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<Organigram> Organigram { get; set; }
        DbSet<OrganigramEmployee> OrganigramEmployee { get; set; }
        DbSet<Region> Region { get; set; }
        DbSet<Site> Site { get; set; }
    }
}
