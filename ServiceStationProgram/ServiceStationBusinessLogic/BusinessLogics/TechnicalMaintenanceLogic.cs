using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;

namespace ServiceStationBusinessLogic.BusinessLogics
{
    public class TechnicalMaintenanceLogic : ITechnicalMaintenanceLogic
    {
        private readonly ITechnicalMaintenanceStorage _technicalMaintenanceStorage;
        public TechnicalMaintenanceLogic(ITechnicalMaintenanceStorage technicalMaintenanceStorage)
        {
            _technicalMaintenanceStorage = technicalMaintenanceStorage;
        }
        public List<TechnicalMaintenanceViewModel> Read(TechnicalMaintenanceBindingModel model)
        {
            if (model == null)
            {
                return _technicalMaintenanceStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TechnicalMaintenanceViewModel> { _technicalMaintenanceStorage.GetElement(model) };
            }
            return _technicalMaintenanceStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TechnicalMaintenanceBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _technicalMaintenanceStorage.Update(model);
            }
            else
            {
                _technicalMaintenanceStorage.Insert(model);
            }
        }
        public void Delete(TechnicalMaintenanceBindingModel model)
        {
            var element = _technicalMaintenanceStorage.GetElement(new TechnicalMaintenanceBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _technicalMaintenanceStorage.Delete(model);
        }
    }
}
