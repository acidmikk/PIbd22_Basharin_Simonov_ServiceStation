using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateIn { get; set; }
        [Required]
        public DateTime DateOut { get; set; }
        [Required]
        public string Discription { get; set; }
        public int DefectId { get; set; }
        public virtual Defect Defect { get; set; }
        public int TechnicalMaintenanceId { get; set; }
        public virtual TechnicalMaintenance TechnicalMaintenance { get; set; }
        public int InspectorId { get; set; }
        public virtual Inspector Inspector { get; set; }
    }
}
