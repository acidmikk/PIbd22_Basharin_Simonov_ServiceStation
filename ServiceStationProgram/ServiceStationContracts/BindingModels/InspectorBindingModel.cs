using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.BindingModels
{
    public class InspectorBindingModel
    {
        public int? Id { get; set; }
        public string InspectorFIO { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
