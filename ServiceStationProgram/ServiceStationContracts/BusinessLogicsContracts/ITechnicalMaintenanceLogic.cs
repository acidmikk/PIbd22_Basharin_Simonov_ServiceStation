using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface ITechnicalMaintenanceLogic
    {
        List<TechnicalMaintenanceViewModel> Read(TechnicalMaintenanceBindingModel model);
        void CreateOrUpdate(TechnicalMaintenanceBindingModel model);
        void Delete(TechnicalMaintenanceBindingModel model);
    }
}
