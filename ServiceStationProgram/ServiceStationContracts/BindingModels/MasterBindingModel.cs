using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BindingModels
{
    public class MasterBindingModel
    {
        public int? Id { get; set; }
        public string MasterFIO { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
