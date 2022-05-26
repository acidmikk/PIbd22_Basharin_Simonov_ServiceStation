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
    public class MasterLogic : IMasterLogic
    {
        private readonly IMasterStorage _MasterStorage;
        public MasterLogic(IMasterStorage MasterStorage)
        {
            _MasterStorage = MasterStorage;
        }
        public List<MasterViewModel> Read(MasterBindingModel model)
        {
            if (model == null)
            {
                return _MasterStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MasterViewModel> { _MasterStorage.GetElement(model) };
            }
            return _MasterStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(MasterBindingModel model)
        {
            var element = _MasterStorage.GetElement(new MasterBindingModel
            {
                Email = model.Email
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть мастер с таким логином");
            }
            if (model.Id.HasValue)
            {
                _MasterStorage.Update(model);
            }
            else
            {
                _MasterStorage.Insert(model);
            }
        }
        public void Delete(MasterBindingModel model)
        {
            var element = _MasterStorage.GetElement(new MasterBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _MasterStorage.Delete(model);
        }
    }
}
