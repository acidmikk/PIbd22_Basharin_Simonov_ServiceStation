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
    public class WorkLogic : IWorkLogic
    {
        private readonly IWorkStorage _workStorage;
        public WorkLogic(IWorkStorage workStorage)
        {
            _workStorage = workStorage;
        }
        public List<WorkViewModel> Read(WorkBindingModel model)
        {
            if (model == null)
            {
                return _workStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WorkViewModel> { _workStorage.GetElement(model) };
            }
            return _workStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(WorkBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _workStorage.Update(model);
            }
            else
            {
                _workStorage.Insert(model);
            }
        }
        public void Delete(WorkBindingModel model)
        {
            var element = _workStorage.GetElement(new WorkBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _workStorage.Delete(model);
        }
    }
}
