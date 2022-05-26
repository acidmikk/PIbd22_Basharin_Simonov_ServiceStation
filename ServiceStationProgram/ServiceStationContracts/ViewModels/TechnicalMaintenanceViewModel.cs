using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class TechnicalMaintenanceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Наименование ТО")]
        public string Name { get; set; }
        [DisplayName("Описание ТО")]
        public string Discription { get; set; }
    }
}
