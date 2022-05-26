using ServiceStationBusinessLogic.BusinessLogics;
using ServiceStationBusinessLogic.OfficePackage;
using ServiceStationBusinessLogic.OfficePackage.Implements;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.StoragesContracts;
using ServiceStationDatabaseImplement.Implements;
using Microsoft.OpenApi.Models;

namespace ServiceStationRestAPI
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
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IClerkStorage, ClerkStorage>();
            services.AddTransient<ICurrencyStorage, CurrencyStorage>();
            services.AddTransient<IDepositStorage, DepositStorage>();
            services.AddTransient<ILoanProgramStorage, LoanProgramStorage>();
            services.AddTransient<IManagerStorage, ManagerStorage>();
            services.AddTransient<IReplenishmentStorage, ReplenishmentStorage>();
            services.AddTransient<ITermStorage, TermStorage>();

            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IClerkLogic, ClerkLogic>();
            services.AddTransient<ICurrencyLogic, CurrencyLogic>();
            services.AddTransient<IDepositLogic, DepositLogic>();
            services.AddTransient<ILoanProgramLogic, LoanProgramLogic>();
            services.AddTransient<IManagerLogic, ManagerLogic>();
            services.AddTransient<IReplenishmentLogic, ReplenishmentLogic>();
            services.AddTransient<ITermLogic, TermLogic>();
            services.AddTransient<IReportLogic, ReportLogic>();
            services.AddTransient<AbstractSaveToWord, SaveToWord>();
            services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
            services.AddTransient<AbstractSaveToPdf, SaveToPdf>();


            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FlowerShopRestApi",
                    Version = "v1"
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlowerShopRestApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
