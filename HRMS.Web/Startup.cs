
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HRMS.Core.Services.Interfaces;
using HRMS.Persistance.Psql;
using HRMS.Core.Services;

namespace HRMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<HRMSContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("HRMS.Persistance")
                ));

            #region SQL SERVER

            // services.AddDbContext<HRMS.Persistance.Psql.HRMSContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //         x => x.MigrationsAssembly("HRMS.Persistance")
            //        ));

            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(
            //         Configuration.GetConnectionString("UsersConnection")));
            // services.AddDefaultIdentity<IdentityUser>()
            //     .AddDefaultUI(UIFramework.Bootstrap4)
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            #endregion

            services.AddMvc();

            RegisterCustomServices(services);
        }

        private void RegisterCustomServices(IServiceCollection services)
        {
            //services.AddTransient<IUniOfWork, UnitOfWork>();
            //services.AddSingleton<IUniOfWork, UnitOfWork>();
            //services.AddScoped<IUniOfWork, UnitOfWork>();
            services.AddScoped<IUniOfWork, UnitOfWork>();
            services.AddScoped<IJsonService, JsonService>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IDepartamentService, DepartmentService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IOrganigramService, OrganigramService>();
            services.AddTransient<ICompanyDepartamentService, CompanyDepartmentService>();
            services.AddTransient<IPayrollSeasonService, PayrollSeasonService>();
            services.AddTransient<IPayrollSegmentService, PayrollSegmentService>();
            services.AddTransient<IPayrollService, PayrollService>();
           
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
