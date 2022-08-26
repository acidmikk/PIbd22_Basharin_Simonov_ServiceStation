using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    //Модель для получения отчёта по машинам (Inspector)
    public class ReportCarsViewModel
    {
        public string CarName { get; set; }
        public DateTime DateIn { get; set; }
        public string DefectName { get; set; }
        public string TechnicalMaintenanceName { get; set; }
    }
}
