using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Models
{
    public class Inspector
    {
        public int Id { get; set; }

        [Required]
        public string InspectorFIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("InspectorId")]
        public virtual List<Car> Cars { get; set; }

        [ForeignKey("InspectorId")]
        public virtual List<Defect> Defects { get; set; }

        [ForeignKey("InspectorId")]
        public virtual List<TechnicalMaintenance> TechnicalMaintenances { get; set; }
    }
}
