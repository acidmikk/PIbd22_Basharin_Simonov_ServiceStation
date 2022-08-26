using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    //Модель для получения списка работ по выбранным машинам (Inspector)
    public class ReportCarWorkViewModel
    {
        public string CarName { get; set; }
        public List<string> Works { get; set; }
    }
}
