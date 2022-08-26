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
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ICarStorage carStorage, IWorkStorage workStorage,
            AbstractSaveToWord saveToWord, AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            _carStorage = carStorage;
            _workStorage = workStorage;
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
            }
            return list;
        }

        public List<ReportCarsViewModel> GetCars(ReportBindingModel model)
        {
            var list = new List<ReportCarsViewModel>();
            var cars = _carStorage.GetFilteredList(new CarBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                InspectorId = model.InspectorId
            });

            foreach (var car in cars)
            {
                var record = new ReportCarsViewModel
                {
                    CarName = car.Name,
                    DateIn = car.DateIn,
                    TechnicalMaintenanceName = car.TechnicalMaintenanceName,
                    DefectName = car.DefectName
                };
                list.Add(record);
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

        public void SaveCarsToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreatePdfInspector(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Сведения по машинам",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Cars = GetCars(model)
            });
        }
    }
}
