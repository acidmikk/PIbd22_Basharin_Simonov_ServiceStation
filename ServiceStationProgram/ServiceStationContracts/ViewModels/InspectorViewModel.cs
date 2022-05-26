using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationContracts.ViewModels
{
    public class InspectorViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО")]
        public string InspectorFIO { get; set; }

        [DisplayName("Логин")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
