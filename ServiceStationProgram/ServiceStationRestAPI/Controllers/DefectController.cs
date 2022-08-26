using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefectController : ControllerBase
    {
        private readonly IDefectLogic _defectLogic;
        private readonly ICarLogic _carLogic;
        private readonly IRepairLogic _repairLogic;

        public DefectController(IDefectLogic defectLogic, ICarLogic carLogic, IRepairLogic repairLogic)
        {
            _defectLogic = defectLogic;
            _carLogic = carLogic;
            _repairLogic = repairLogic;
        }

        [HttpGet]
        public List<DefectViewModel> GetDefectList() => _defectLogic.Read(null)?.ToList();

        [HttpGet]
        public List<CarViewModel> GetCars() => _carLogic.Read(null)?.ToList();

        [HttpGet]
        public CarViewModel GetCar(int carId) => _carLogic.Read(new CarBindingModel { Id = carId })?[0];

        [HttpGet]
        public DefectViewModel GetDefect(int defectId) => _defectLogic.Read(new DefectBindingModel { Id = defectId })?[0];

        [HttpPost]
        public void CreateOrUpdateDefect(DefectBindingModel model) => _defectLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteDefect(DefectBindingModel model) => _defectLogic.Delete(model);

        [HttpGet]
        public List<RepairViewModel> GetRepairList() => _repairLogic.Read(null)?.ToList();

        [HttpGet]
        public RepairViewModel GetRepair(int repairId) => _repairLogic.Read(new RepairBindingModel { Id = repairId })?[0];

        [HttpPost]
        public void AddDefectRepair(AddDefectRepairBindingModel model) => _defectLogic.AddRepair(model);
    }
}
