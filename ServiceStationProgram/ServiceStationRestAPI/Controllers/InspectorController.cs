using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InspectorController : ControllerBase
    {
        private readonly IInspectorLogic _inspectorLogic;
        private readonly IDefectLogic _defectLogic;
        private readonly ITechnicalMaintenanceLogic _technicalMaintenanceLogic;
        private readonly ICarLogic _carLogic;
        public InspectorController(IInspectorLogic logic, IDefectLogic defectLogic, ITechnicalMaintenanceLogic technicalMaintenanceLogic, ICarLogic carLogic)
        {
            _inspectorLogic = logic;
            _defectLogic = defectLogic;
            _technicalMaintenanceLogic = technicalMaintenanceLogic;
            _carLogic = carLogic;
        }

        [HttpGet]
        public InspectorViewModel Login(string login, string password)
        {
            var list = _inspectorLogic.Read(new InspectorBindingModel
            {
                Email = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(InspectorBindingModel model) => _inspectorLogic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(InspectorBindingModel model) => _inspectorLogic.CreateOrUpdate(model);

        [HttpGet]
        public List<DefectViewModel> GetInspectorDefectList(int inspectorId) => _defectLogic.Read(new DefectBindingModel { InspectorId = inspectorId });

        [HttpGet]
        public List<CarViewModel> GetInspectorCarList(int inspectorId) => _carLogic.Read(new CarBindingModel { InspectorId = inspectorId });

        [HttpGet]
        public List<TechnicalMaintenanceViewModel> GetInsppectorTechnicalMaintenanceList(int inspectorId) => 
            _technicalMaintenanceLogic.Read(new TechnicalMaintenanceBindingModel { InspectorId = inspectorId });
    }
}
