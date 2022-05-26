using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.StoragesContracts
{
    public interface IRepairStorage
    {
        List<RepairViewModel> GetFullList();

        List<RepairViewModel> GetFilteredList(RepairBindingModel model);

        RepairViewModel GetElement(RepairBindingModel model);

        void Insert(RepairBindingModel model);

        void Update(RepairBindingModel model);

        void Delete(RepairBindingModel model);
    }
}
