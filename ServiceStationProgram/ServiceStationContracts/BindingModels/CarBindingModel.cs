using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BindingModels
{
    public class CarBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public string Discription { get; set; }
        public int? DefectId { get; set; }
        public int? TechnicalMaintenanceId { get; set; }
        public int? InspectorId { get; set; }
    }
}
