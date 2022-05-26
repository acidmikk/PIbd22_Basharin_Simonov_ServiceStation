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
    public class InspectorLogic : IInspectorLogic
    {
        private readonly IInspectorStorage _InspectorStorage;
        public InspectorLogic(IInspectorStorage InspectorStorage)
        {
            _InspectorStorage = InspectorStorage;
        }
        public List<InspectorViewModel> Read(InspectorBindingModel model)
        {
            if (model == null)
            {
                return _InspectorStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<InspectorViewModel> { _InspectorStorage.GetElement(model) };
            }
            return _InspectorStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(InspectorBindingModel model)
        {
            var element = _InspectorStorage.GetElement(new InspectorBindingModel
            {
                Email = model.Email
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть приемщик с таким логином");
            }
            if (model.Id.HasValue)
            {
                _InspectorStorage.Update(model);
            }
            else
            {
                _InspectorStorage.Insert(model);
            }
        }
        public void Delete(InspectorBindingModel model)
        {
            var element = _InspectorStorage.GetElement(new InspectorBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _InspectorStorage.Delete(model);
        }
    }
}
