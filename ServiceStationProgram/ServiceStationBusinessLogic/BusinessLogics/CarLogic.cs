using ServiceStationContracts.BindingModels;
using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;
using ServiceStationContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationBusinessLogic.BusinessLogics
{
    public class CarLogic : ICarLogic
    {
        private readonly ICarStorage _carStorage;
        public CarLogic(ICarStorage carStorage)
        {
            _carStorage = carStorage;
        }
        public List<CarViewModel> Read(CarBindingModel model)
        {
            if (model == null)
            {
                return _carStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CarViewModel> { _carStorage.GetElement(model) };
            }
            return _carStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(CarBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _carStorage.Update(model);
            }
            else
            {
                _carStorage.Insert(model);
            }
        }
        public void Delete(CarBindingModel model)
        {
            var element = _carStorage.GetElement(new CarBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _carStorage.Delete(model);
        }
    }
}
