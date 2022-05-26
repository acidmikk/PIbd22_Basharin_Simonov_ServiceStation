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
        private readonly ILoanProgramLogic _loanProgramLogic;

        public DefectController(IDefectLogic defectLogic, ILoanProgramLogic loanProgramLogic)
        {
            _defectLogic = defectLogic;
            _loanProgramLogic = loanProgramLogic;
        }

        [HttpGet]
        public List<ClientViewModel> GetClientList() => _defectLogic.Read(null)?.ToList();

        [HttpGet]
        public List<LoanProgramViewModel> GetLoanProgramList() => _loanProgramLogic.Read(null)?.ToList();

        [HttpGet]
        public LoanProgramViewModel GetLoanProgram(int loanProgramId) => _loanProgramLogic.Read(new LoanProgramBindingModel { Id = loanProgramId })?[0];

        [HttpGet]
        public ClientViewModel GetClient(int clientId) => _defectLogic.Read(new ClientBindingModel { Id = clientId })?[0];

        [HttpPost]
        public void CreateOrUpdateClient(ClientBindingModel model) => _defectLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteClient(ClientBindingModel model) => _defectLogic.Delete(model);
    }
}
}
