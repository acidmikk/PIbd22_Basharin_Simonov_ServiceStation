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
    public class SparesLogic : ISparesLogic
    {
        private readonly ISparesStorage _sparesStorage;
        public SparesLogic(ISparesStorage sparesStorage)
        {
            _sparesStorage = sparesStorage;
        }
        public List<SparesViewModel> Read(SparesBindingModel model)
        {
            if (model == null)
            {
                return _sparesStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SparesViewModel> { _sparesStorage.GetElement(model) };
            }
            return _sparesStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(SparesBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _sparesStorage.Update(model);
            }
            else
            {
                _sparesStorage.Insert(model);
            }
        }
        public void Delete(SparesBindingModel model)
        {
            var element = _sparesStorage.GetElement(new SparesBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _sparesStorage.Delete(model);
        }
    }
}
