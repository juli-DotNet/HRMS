using System.Linq;
using HRMS.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Persostance.Psql
{
    public class HRMSContext : DbContext
    {
        public HRMSContext(DbContextOptions<HRMSContext> options)
            : base(options)
        {
        }

        public HRMSContext()
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyDepartament> CompanyDepartament { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Organigram> Organigram { get; set; }
        public DbSet<OrganigramEmployee> OrganigramEmployee { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Departament> Departament { get; set; }
        public DbSet<PayrollSegment> PayrollSegment { get; set; }
        public DbSet<PayrollSeason> PayrollSeason { get; set; }
        public DbSet<CompanyPayroll> CompanyPayroll { get; set; }
        public DbSet<EmployeeCompanyPayroll> EmployeeCompanyPayroll { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

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