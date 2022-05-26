using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.StoragesContracts
{
    public interface ISparesStorage
    {
        List<SparesViewModel> GetFullList();

        List<SparesViewModel> GetFilteredList(SparesBindingModel model);

        SparesViewModel GetElement(SparesBindingModel model);

        void Insert(SparesBindingModel model);

        void Update(SparesBindingModel model);

        void Delete(SparesBindingModel model);
    }
}
