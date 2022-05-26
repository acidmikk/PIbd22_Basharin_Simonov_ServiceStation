using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TechnicalMaintenanceController : ControllerBase
    {
        private readonly ITechnicalMaintenanceLogic _technicalMaintenanceLogic;
        private readonly ICarLogic _carLogic;

        public TechnicalMaintenanceController(ITechnicalMaintenanceLogic technicalMaintenanceLogic, ICarLogic carLogic)
        {
            _technicalMaintenanceLogic = technicalMaintenanceLogic;
            _carLogic = carLogic;
        }

        [HttpGet]
        public List<TechnicalMaintenanceViewModel> GetTechnicalMaintenanceList() => _technicalMaintenanceLogic.Read(null)?.ToList();

        [HttpGet]
        public List<CarViewModel> GetCars() => _carLogic.Read(null)?.ToList();

        [HttpGet]
        public CarViewModel GetCar(int carId) => _carLogic.Read(new CarBindingModel { Id = carId })?[0];

        [HttpGet]
        public TechnicalMaintenanceViewModel GetTechnicalMaintenance(int technicalMaintenanceId) => 
            _technicalMaintenanceLogic.Read(new TechnicalMaintenanceBindingModel { Id = technicalMaintenanceId })?[0];

        [HttpPost]
        public void CreateOrUpdateTechnicalMaintenance(TechnicalMaintenanceBindingModel model) => _technicalMaintenanceLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteTechnicalMaintenance(TechnicalMaintenanceBindingModel model) => _technicalMaintenanceLogic.Delete(model);
    }
}
