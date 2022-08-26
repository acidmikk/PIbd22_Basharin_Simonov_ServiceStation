using ServiceStationBusinessLogic.OfficePackage;
using ServiceStationBusinessLogic.OfficePackage.HelperModels;
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
    public class ReportLogic : IReportLogic
    {
        private readonly ICarStorage _carStorage;
        private readonly IWorkStorage _workStorage;
        private readonly ITechnicalMaintenanceStorage _technicalMaintenanceStorage;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ICarStorage carStorage, IWorkStorage workStorage, ITechnicalMaintenanceStorage technicalMaintenanceStorage,
            AbstractSaveToWord saveToWord, AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            _carStorage = carStorage;
            _workStorage = workStorage;
            _technicalMaintenanceStorage = technicalMaintenanceStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        public List<ReportCarWorkViewModel> GetCarWork(ReportBindingModel model)
        {
            var cars = model.Cars;
            var list = new List<ReportCarWorkViewModel>();
            foreach (var car in cars)
            {
                var record = new ReportCarWorkViewModel
                {
                    CarName = car.Name,
                    Works = new List<string>()
                };

                var works = _workStorage.GetFullList().Where(rec => rec.TechnicalMaintenanceId == car.TechnicalMaintenanceId);

                foreach (var work in works)
                {
                    record.Works.Add(work.Name);
                }
                record.Works = record.Works.Distinct().ToList();
                list.Add(record);

                /*
                foreach (var loanProgramKVP in car.ClientLoanPrograms)
                {
                    var lp = _loanProgramStorage.GetElement(new LoanProgramBindingModel { Id = loanProgramKVP.Key });
                    foreach (var currency in lp.LoanProgramCurrencies)
                    {
                        record.Currencies.Add(currency.Value.Item1);
                    }
                }
                var deposits = _depositStorage.GetFullList().Where(rec => rec.DepositClients.Keys.ToList().Contains(car.Id)).ToList();
                foreach (var deposit in deposits)
                {
                    var currencies = _currencyStorage.GetFullList().Where(rec => rec.CurrencyDeposits.Keys.ToList().Contains(deposit.Id)).ToList();
                    record.Currencies.AddRange(currencies.Select(cur => cur.CurrencyName));
                }
                */
            }
            return list;
        }

        public void SaveCarWorkToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDocInspector(new WordInfo
            {
                FileName = model.FileName,
                Title = "Работы по машинам",
                CarWork = GetCarWork(model),
            });
        }

        public void SaveCarWorkToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateExcelInspector(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Работы по машинам",
                CarWork = GetCarWork(model),
            });
        }
    }
}
