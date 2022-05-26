using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SparesId { get; set; }
        public virtual Spares Spares { get; set; }
        public int TechnicalMaintenanceId { get; set; }
        public virtual TechnicalMaintenance TechnicalMaintenance { get; set; }
        public int MasterId { get; set; }
        public virtual Master Master { get; set; }
    }
}
