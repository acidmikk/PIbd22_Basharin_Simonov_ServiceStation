using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class WorkViewModel
    {
        public int Id { get; set; }
        [DisplayName("Наименование работы")]
        public string Name { get; set; }
        public int? SparesId { get; set; }
        public int? TechnicalMaintenanceId { get; set; }
    }
}
