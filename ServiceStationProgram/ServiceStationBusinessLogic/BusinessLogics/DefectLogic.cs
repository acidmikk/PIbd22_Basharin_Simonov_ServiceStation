using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationBusinessLogic.BusinessLogics
{
    public class DefectLogic : IDefectLogic
    {
        private readonly IDefectStorage _defectStorage;
        public DefectLogic(IDefectStorage defectStorage)
        {
            _defectStorage = defectStorage;
        }
        public List<DefectViewModel> Read(DefectBindingModel model)
        {
            if (model == null)
            {
                return _defectStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DefectViewModel> { _defectStorage.GetElement(model) };
            }
            return _defectStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DefectBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _defectStorage.Update(model);
            }
            else
            {
                _defectStorage.Insert(model);
            }
        }
        public void Delete(DefectBindingModel model)
        {
            var element = _defectStorage.GetElement(new DefectBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _defectStorage.Delete(model);
        }
    }
}
