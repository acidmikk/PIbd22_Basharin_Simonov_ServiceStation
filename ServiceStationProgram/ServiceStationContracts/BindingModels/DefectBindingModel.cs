using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BindingModels
{
    public class DefectBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public Dictionary<int, string> DefectCars { get; set; }
        public int? RepairId { get; set; }
        public int? InspectorId { get; set; }
    }
}
