using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        private readonly ICarLogic _carLogic;
        public ReportController(IReportLogic reportLogic, ICarLogic carLogic)
        {
            _reportLogic = reportLogic;
            _carLogic = carLogic;
        }
        
        [HttpPost]
        public void CreateReportCarWorkToWordFile(ReportBindingModel model) => _reportLogic.SaveCarWorkToWordFile(model);

        [HttpPost]
        public void CreateReportCarWorkToExcelFile(ReportBindingModel model) => _reportLogic.SaveCarWorkToExcelFile(model);
        
        [HttpPost]
        public void CreateReportCarsToPdfFile(ReportBindingModel model) => _reportLogic.SaveCarsToPdfFile(model);
        
        [HttpGet]
        public List<ReportCarsViewModel> GetCarsReport(string dateFrom, string dateTo) => _reportLogic.GetCars(new ReportBindingModel { DateFrom = Convert.ToDateTime(dateFrom), DateTo = Convert.ToDateTime(dateTo) });
    }
}
