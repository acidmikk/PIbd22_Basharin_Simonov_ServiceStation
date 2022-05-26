using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface ISparesLogic
    {
        List<SparesViewModel> Read(SparesBindingModel model);
        void CreateOrUpdate(SparesBindingModel model);
        void Delete(SparesBindingModel model);
    }
}
