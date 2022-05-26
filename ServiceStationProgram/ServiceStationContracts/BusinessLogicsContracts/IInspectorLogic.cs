using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface IInspectorLogic
    {
        List<InspectorViewModel> Read(InspectorBindingModel model);
        void CreateOrUpdate(InspectorBindingModel model);
        void Delete(InspectorBindingModel model);
    }
}
