using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название автомобиля")]
        public string Name { get; set; }
        [DisplayName("Дата начала работ")]
        public DateTime DateIn { get; set; }
        [DisplayName("Дата окончания работ")]
        public DateTime DateOut { get; set; }
        [DisplayName("Описание")]
        public string Discription { get; set; }
        public int DefectId { get; set; }
        public int TechnicalMaintenanceId { get; set; }
    }
}
