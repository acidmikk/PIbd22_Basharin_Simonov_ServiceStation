using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Models
{
    public class Repair
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("RepairId")]
        public virtual List<Spares> Spares { get; set; }
        public int MasterId { get; set; }
        public virtual Master Master { get; set; }
    }
}
