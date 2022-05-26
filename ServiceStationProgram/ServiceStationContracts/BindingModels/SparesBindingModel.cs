using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BindingModels
{
    public class SparesBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? DefectId { get; set; }
        public int? RepairId { get; set; }
        public int? MasterId { get; set; }
    }
}
