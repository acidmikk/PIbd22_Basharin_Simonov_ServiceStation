using ServiceStationBusinessLogic.BusinessLogics;
using ServiceStationBusinessLogic.OfficePackage;
using ServiceStationBusinessLogic.OfficePackage.Implements;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.StoragesContracts;
using Microsoft.OpenApi.Models;
using ServiceStationDatabaseImplement.Implements;

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
            services.AddTransient<ICarStorage, CarStorage>();
            services.AddTransient<IDefectStorage, DefectStorage>();
            services.AddTransient<IInspectorStorage, InspectorStorage>();
            services.AddTransient<IRepairStorage, RepairStorage>();
            services.AddTransient<ISparesStorage, SparesStorage>();
            services.AddTransient<ITechnicalMaintenanceStorage, TechnicalMaintenanceStorage>();
            services.AddTransient<IWorkStorage, WorkStorage>();
            services.AddTransient<IMasterStorage, MasterStorage>();

            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<IDefectLogic, DefectLogic>();
            services.AddTransient<IInspectorLogic, InspectorLogic>();
            services.AddTransient<IRepairLogic, RepairLogic>();
            services.AddTransient<ISparesLogic, SparesLogic>();
            services.AddTransient<ITechnicalMaintenanceLogic, TechnicalMaintenanceLogic>();
            services.AddTransient<IMasterLogic, MasterLogic>();
            services.AddTransient<IWorkLogic, WorkLogic>();
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
