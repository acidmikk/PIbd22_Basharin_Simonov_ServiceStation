using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Models
{
    public class Master
    {
        public int Id { get; set; }

        [Required]
        public string MasterFIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("MasterId")]
        public virtual List<Spares> Spares { get; set; }

        [ForeignKey("MasterId")]
        public virtual List<Repair> Repairs { get; set; }

        [ForeignKey("MasterId")]
        public virtual List<Work> Works { get; set; }
    }
}
