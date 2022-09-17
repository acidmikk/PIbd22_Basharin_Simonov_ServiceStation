using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Models
{
    public class Spares
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DefectId { get; set; }
        public virtual Defect Defect { get; set; }
        [ForeignKey("SparesId")]
        public virtual List<Work> Works { get; set; }
        public int? RepairId { get; set; }
        public virtual Repair? Repair { get; set; }
        public int MasterId { get; set; }
        public virtual Master Master { get; set; }
    }
}
