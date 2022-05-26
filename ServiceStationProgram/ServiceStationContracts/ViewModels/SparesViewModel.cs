using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class SparesViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название запчасти")]
        public string Name { get; set; }
        public int? DefectId { get; set; }
        public int? RepairId { get; set; }
    }
}
