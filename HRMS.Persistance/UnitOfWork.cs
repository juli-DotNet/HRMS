using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace HRMS.Persistance
{
    public class UnitOfWork : IUniOfWork
    {
        private readonly HRMSContext _context;

        public UnitOfWork(HRMSContext context)
        {
            _context = context;
        }
        public IRepository<Address> Address => new Repository<Address>(_context);

        public IRepository<City> City => new Repository<City>(_context);

        public IRepository<Company> Company => new Repository<Company>(_context);

        public IRepository<CompanySite> CompanySite => new Repository<CompanySite>(_context);

        public IRepository<Country> Country => new Repository<Country>(_context);

        public IRepository<Employee> Employee => new Repository<Employee>(_context);

        public IRepository<Organigram> Organigram => new Repository<Organigram>(_context);

        public IRepository<OrganigramEmployee> OrganigramEmployee => new Repository<OrganigramEmployee>(_context);

        public IRepository<Region> Region => new Repository<Region>(_context);

        public IRepository<Site> Site => new Repository<Site>(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
