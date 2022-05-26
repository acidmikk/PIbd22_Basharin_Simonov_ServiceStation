using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.StoragesContracts
{
    public interface IWorkStorage
    {
        List<WorkViewModel> GetFullList();

        List<WorkViewModel> GetFilteredList(WorkBindingModel model);

        WorkViewModel GetElement(WorkBindingModel model);

        void Insert(WorkBindingModel model);

        void Update(WorkBindingModel model);

        void Delete(WorkBindingModel model);
    }
}
