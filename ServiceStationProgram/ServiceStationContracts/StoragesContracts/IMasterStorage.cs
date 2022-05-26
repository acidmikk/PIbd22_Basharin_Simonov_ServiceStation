using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.StoragesContracts
{
    public interface IMasterStorage
    {
        List<MasterViewModel> GetFullList();

        List<MasterViewModel> GetFilteredList(MasterBindingModel model);

        MasterViewModel GetElement(MasterBindingModel model);

        void Insert(MasterBindingModel model);

        void Update(MasterBindingModel model);

        void Delete(MasterBindingModel model);
    }
}
