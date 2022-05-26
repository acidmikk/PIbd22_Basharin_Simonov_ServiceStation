using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface IMasterLogic
    {
        List<MasterViewModel> Read(MasterBindingModel model);
        void CreateOrUpdate(MasterBindingModel model);
        void Delete(MasterBindingModel model);
    }
}
