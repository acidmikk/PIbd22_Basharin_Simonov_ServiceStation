using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {

        //Inspector

        // Получение списка работ по выбранным машинам
        List<ReportCarWorkViewModel> GetCarWork(ReportBindingModel model);

        // Сохранение работ по выбранным машинам в файл-Word
        void SaveCarWorkToWordFile(ReportBindingModel model);

        // Сохранение работ по выбранным машинам в файл-Excel
        void SaveCarWorkToExcelFile(ReportBindingModel model);

        // Получение списка машин за период
        List<ReportCarsViewModel> GetCars(ReportBindingModel model);

        // Сохранение списка машин за период в файл-Pdf
        void SaveCarsToPdfFile(ReportBindingModel model);
    }
}

