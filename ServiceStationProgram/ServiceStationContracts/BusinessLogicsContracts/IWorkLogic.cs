using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface IWorkLogic
    {
        List<WorkViewModel> Read(WorkBindingModel model);
        void CreateOrUpdate(WorkBindingModel model);
        void Delete(WorkBindingModel model);
    }
}
