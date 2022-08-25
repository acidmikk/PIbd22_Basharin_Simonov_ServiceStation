using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceStationBusinessLogic.BusinessLogics
{
    public class InspectorLogic : IInspectorLogic
    {
        private readonly IInspectorStorage _InspectorStorage;
        private readonly int _emailMaxLength = 50;
        private readonly int _passwordMaxLength = 30;
        private readonly int _passwordMinLength = 10;
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
            if (model.Email.Length > _emailMaxLength || !Regex.IsMatch(model.Email, @"([a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+)"))
            {
                throw new Exception($"В качестве логина должна быть указана почта и иметь длинну не более {_emailMaxLength} символов");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength
                || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
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
