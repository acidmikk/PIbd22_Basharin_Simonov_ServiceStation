using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class RepairViewModel
    {
        public int Id { get; set; }
        [DisplayName("Наименование ремонта")]
        public string Name { get; set; }

    }
}
