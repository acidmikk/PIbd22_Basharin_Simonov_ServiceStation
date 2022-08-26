using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BindingModels
{
    public class AddDefectRepairBindingModel
    {
        public int DefectId { get; set; }
        public int RepairId { get; set; }
    }
}
