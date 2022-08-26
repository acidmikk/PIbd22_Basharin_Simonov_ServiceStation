using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class DefectViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название неисправности")]
        public string Name { get; set; }
        [DisplayName("Описание неиспраности")]
        public string Discription { get; set; }
        public Dictionary<int, string> DefectCars { get; set; }
        public int? RepairId { get; set; }
    }
}
