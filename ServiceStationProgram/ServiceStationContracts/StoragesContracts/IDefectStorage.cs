using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.StoragesContracts
{
    public interface IDefectStorage
    {
        List<DefectViewModel> GetFullList();

        List<DefectViewModel> GetFilteredList(DefectBindingModel model);

        DefectViewModel GetElement(DefectBindingModel model);

        void Insert(DefectBindingModel model);

        void Update(DefectBindingModel model);

        void Delete(DefectBindingModel model);
    }
}
