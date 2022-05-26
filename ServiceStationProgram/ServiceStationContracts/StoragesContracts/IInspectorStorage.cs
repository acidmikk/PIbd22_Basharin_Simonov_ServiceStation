using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.StoragesContracts
{
    public interface IInspectorStorage
    {
        List<InspectorViewModel> GetFullList();

        List<InspectorViewModel> GetFilteredList(InspectorBindingModel model);

        InspectorViewModel GetElement(InspectorBindingModel model);

        void Insert(InspectorBindingModel model);

        void Update(InspectorBindingModel model);

        void Delete(InspectorBindingModel model);
    }
}
