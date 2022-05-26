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

        public DefectController(IDefectLogic defectLogic, ICarLogic carLogic)
        {
            _defectLogic = defectLogic;
            _carLogic = carLogic;
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
    }
}
