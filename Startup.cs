using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecoveryAppLibrary.Data;
using RecoveryAppLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecoveryMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //configuring the connection string data - lives across entire application 
            services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Default"
            });
            services.AddSingleton<IDataAccess, SqlDb>();
            //registered repository services
            services.AddScoped<IOrganizationData, OrganizationData>();
            services.AddScoped<IManagerData, ManagerData>();
            services.AddScoped<ITenantData, TenantData>();
            services.AddScoped<ITenantDetailsData, TenantDetailsData>();
            services.AddScoped<IEmergencyContactData, EmergencyContactData>();
            services.AddScoped<IFinesHistoryData, FinesHistoryData>();
            services.AddScoped<IPaymentHistoryData, PaymentHistoryData>();
            services.AddScoped<IPreferredHospitalData, PreferredHospitalData>();
            services.AddScoped<IRentAdjustmentData, RentAdjustmentData>();
            services.AddScoped<IUAResultData, UAResultData>();
            services.AddScoped<IUAFollowUpData, UAFollowUpData>();
            services.AddScoped<IIncidentReportData, IncidentReportData>();
            services.AddScoped<IIncidentFollowUpData, IIncidentFollowUpData>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            //Default route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
