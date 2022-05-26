using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface IDefectLogic
    {
        List<DefectViewModel> Read(DefectBindingModel model);
        void CreateOrUpdate(DefectBindingModel model);
        void Delete(DefectBindingModel model);
    }
}
