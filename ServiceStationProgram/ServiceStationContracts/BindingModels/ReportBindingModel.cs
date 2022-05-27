using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStationContracts.ViewModels;

namespace ServiceStationContracts.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<CarViewModel>? Cars { get; set; }
        public List<WorkViewModel>? Works { get; set; }
        public int MasterId { get; set; }
        public int InspectorId { get; set; }
    }
}
