using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class ReportRepairWorkViewModel
    {
        public string Repair { get; set; }
        public string WorkName { get; set; }
        public List<Tuple<string>> Spares { get; set; }
    }
}
