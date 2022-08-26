using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStationContracts.ViewModels;

namespace ServiceStationBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportCarWorkViewModel> CarWork { get; set; }

        //public List<ReportLoanProgramDepositViewModel> LoanProgramDeposit { get; set; }
    }
}
