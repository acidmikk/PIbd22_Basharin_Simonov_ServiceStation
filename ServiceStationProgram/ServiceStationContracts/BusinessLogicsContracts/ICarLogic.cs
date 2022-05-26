using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface ICarLogic
    {
        List<CarViewModel> Read(CarBindingModel model);
        void CreateOrUpdate(CarBindingModel model);
        void Delete(CarBindingModel model);
    }
}
