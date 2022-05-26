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
    public class RepairLogic : IRepairLogic
    {
        private readonly IRepairStorage _repairStorage;
        public RepairLogic(IRepairStorage repairStorage)
        {
            _repairStorage = repairStorage;
        }
        public List<RepairViewModel> Read(RepairBindingModel model)
        {
            if (model == null)
            {
                return _repairStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<RepairViewModel> { _repairStorage.GetElement(model) };
            }
            return _repairStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(RepairBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _repairStorage.Update(model);
            }
            else
            {
                _repairStorage.Insert(model);
            }
        }
        public void Delete(RepairBindingModel model)
        {
            var element = _repairStorage.GetElement(new RepairBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _repairStorage.Delete(model);
        }
    }
}
