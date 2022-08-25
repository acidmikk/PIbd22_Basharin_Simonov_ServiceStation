using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class ReportCarViewModel
    {
        public string CarName { get; set; }
        public string TechnicalMaintenanceName { get; set; }
        public List<Tuple<string>> Defects { get; set; }
    }
}
