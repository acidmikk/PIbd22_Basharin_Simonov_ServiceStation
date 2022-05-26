using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Models
{
    public class Defect
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Discription { get; set; }
        [ForeignKey("DefectId")]
        public virtual List<Spares> Spares { get; set; }
        [ForeignKey("DefectId")]
        public virtual List<Car> Cars { get; set; }
        public int RepairId { get; set; }
        public virtual Repair Repair { get; set; }
        public int InspectorId { get; set; }
        public virtual Inspector Inspector { get; set; }
    }
}
