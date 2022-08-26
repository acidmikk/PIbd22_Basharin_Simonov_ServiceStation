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
        private readonly IRepairStorage _repairStorage;
        public DefectLogic(IDefectStorage defectStorage, IRepairStorage repairStorage)
        {
            _defectStorage = defectStorage;
            _repairStorage = repairStorage;
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

        public void AddRepair(AddDefectRepairBindingModel model)
        {
            var defect = _defectStorage.GetElement(new DefectBindingModel
            {
                Id = model.DefectId
            });

            if (defect == null)
            {
                throw new Exception("Неисправность не найдена");
            }

            var repair = _repairStorage.GetElement(new RepairBindingModel
            {
                Id = model.RepairId
            });

            if (repair == null)
            {
                throw new Exception("Ремонт не найден");
            }

            defect.RepairId = repair.Id;

            _defectStorage.Update(new DefectBindingModel
            {
                Id = defect.Id,
                Name = defect.Name,
                Discription = defect.Discription,
                InspectorId = defect.InspectorId,                
                RepairId = defect.RepairId
            });
        }
    }
}
