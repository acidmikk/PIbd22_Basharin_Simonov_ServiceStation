using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.StoragesContracts
{
    public interface ITechnicalMaintenanceStorage
    {
        List<TechnicalMaintenanceViewModel> GetFullList();

        List<TechnicalMaintenanceViewModel> GetFilteredList(TechnicalMaintenanceBindingModel model);

        TechnicalMaintenanceViewModel GetElement(TechnicalMaintenanceBindingModel model);

        void Insert(TechnicalMaintenanceBindingModel model);

        void Update(TechnicalMaintenanceBindingModel model);

        void Delete(TechnicalMaintenanceBindingModel model);
    }
}
