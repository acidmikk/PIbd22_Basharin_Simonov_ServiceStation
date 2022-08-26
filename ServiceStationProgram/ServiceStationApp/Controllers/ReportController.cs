using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStationBusinessLogic.Mail;
using ServiceStationContracts.BindingModels;
using ServiceStationContracts.ViewModels;

namespace ServiceStationApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly MailKitWorker _mailKitWorker;

        public ReportController(ILogger<ReportController> logger, IWebHostEnvironment environment, MailKitWorker mailKitWorker)
        {
            _logger = logger;
            _environment = environment;
            _mailKitWorker = mailKitWorker;
        }

        public IActionResult ReportWordExcel()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Cars = APIInspector.GetRequest<List<CarViewModel>>($"api/inspector/GetInspectorCarList?inspectorId={Program.Inspector.Id}");
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateReportCarWorkToWordFile(List<int> carsId)
        {
            if (carsId.Count != 0)
            {
                var model = new ReportBindingModel
                {
                    Cars = new List<CarViewModel>()
                };
                foreach (var carId in carsId)
                {
                    model.Cars.Add(APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}"));
                }
                model.FileName = @"..\ServiceStationApp\wwwroot\ReportCarWork\ReportCarWorkDoc.doc";
                APIInspector.PostRequest("api/report/CreateReportCarWorkToWordFile", model);
                var fileName = "ReportCarWorkDoc.doc";
                var filePath = _environment.WebRootPath + @"\ReportCarWork\" + fileName;
                return PhysicalFile(filePath, "application/doc", fileName);
            }
            throw new Exception("Выберите хотя бы одну машину");
        }

        [HttpPost]
        public IActionResult CreateReportCarWorkToExcelFile(List<int> carsId)
        {
            if (carsId.Count != 0)
            {
                var model = new ReportBindingModel
                {
                    Cars = new List<CarViewModel>()
                };
                foreach (var carId in carsId)
                {
                    model.Cars.Add(APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}"));
                }
                model.FileName = @"..\ServiceStationApp\wwwroot\ReportCarWork\ReportCarWorkExcel.xls";
                APIInspector.PostRequest("api/report/CreateReportCarWorkToExcelFile", model);
                var fileName = "ReportCarWorkExcel.xls";
                var filePath = _environment.WebRootPath + @"\ReportCarWork\" + fileName;
                return PhysicalFile(filePath, "application/xls", fileName);
            }
            throw new Exception("Выберите хотя бы одну машину");
        }
               
        public IActionResult ReportPDF()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ReportGetClientsPDF(DateTime dateFrom, DateTime dateTo)
        {
            ViewBag.Period = "C " + dateFrom.ToLongDateString() + " по " + dateTo.ToLongDateString();
            ViewBag.Report = APIInspector.GetRequest<List<ReportCarsViewModel>>($"api/report/GetCarsReport?dateFrom={dateFrom.ToLongDateString()}&dateTo={dateTo.ToLongDateString()}");
            return View("ReportPdf");
        }

        [HttpPost]
        public IActionResult SendReportOnMail(DateTime dateFrom, DateTime dateTo)
        {
            var model = new ReportBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            model.FileName = @"..\ServiceStationApp\wwwroot\ReportCarWork\ReportCarWorkPdf.pdf";
            APIInspector.PostRequest("api/report/CreateReportCarsToPdfFile", model);
            _mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = Program.Inspector.Email,
                Subject = "Отчет по машинам. СТО \"Руки-крюки\"",
                Text = "Отчет по машинам с " + dateFrom.ToShortDateString() + " по " + dateTo.ToShortDateString() +
                "\nИсполнитель: " + Program.Inspector.InspectorFIO,
                FileName = model.FileName
            });
            return View();
        }
    }
}
