using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BindingModels
{
    public class WorkBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? SparesId { get; set; }
        public int? TechnicalMaintenanceId { get; set; }
        public int? MasterId { get; set; }
    }
}
