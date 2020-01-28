using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HRMS.Persistance
{
    public class HRMSContext : DbContext
    {
        public HRMSContext(DbContextOptions<HRMSContext> options)
            : base(options)
        { }
        public HRMSContext()
        {

        }

        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanySite> CompanySite { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Organigram> Organigram { get; set; }
        public DbSet<OrganigramEmployee> OrganigramEmployee { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Site> Site { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
