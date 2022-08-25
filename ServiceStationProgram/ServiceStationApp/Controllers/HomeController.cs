using Microsoft.AspNetCore.Mvc;
using ServiceStationApp.Models;
using ServiceStationContracts.ViewModels;
using ServiceStationContracts.BindingModels;
using System.Diagnostics;

namespace ServiceStationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Inspector);
        }

        [HttpPost]
        public void Privacy(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio))
            {
                APIInspector.PostRequest("api/inspector/updatedata", new InspectorBindingModel
                {
                    Id = Program.Inspector.Id,
                    InspectorFIO = fio,
                    Email = login,
                    Password = password
                });
                Program.Inspector.InspectorFIO = fio;
                Program.Inspector.Email = login;
                Program.Inspector.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Inspector = APIInspector.GetRequest<InspectorViewModel>($"api/inspector/login?login={login}&password={password}");
                if (Program.Inspector == null)
                {
                    throw new Exception("Неверный логин или пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio))
            {
                APIInspector.PostRequest("api/inspector/register", new InspectorBindingModel
                {
                    InspectorFIO = fio,
                    Email = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        public IActionResult Car()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIInspector.GetRequest<List<CarViewModel>>($"api/inspector/GetInspectorCarList?inspectorId={Program.Inspector.Id}"));
        }

        [HttpGet]
        public IActionResult CarCreate()
        {            
            return View();
        }

        [HttpPost]
        public void CarCreate(string name, string discription, DateTime dateIn)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(discription))
            {
                APIInspector.PostRequest("api/car/CreateOrUpdateCar", new CarBindingModel
                {
                    Name = name,
                    Discription = discription,
                    DateIn = DateTime.Now,
                    InspectorId = Program.Inspector.Id
                });
                Response.Redirect("Car");
                return;
            }
            throw new Exception("Введите название авто и его описание");
        }

        [HttpGet]
        public IActionResult CarUpdate(int carId)
        {
            ViewBag.Car = APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}");
            //ViewBag.LoanPrograms = APIClerk.GetRequest<List<LoanProgramViewModel>>("api/client/GetLoanProgramList");
            //var Deposits = APIClerk.GetRequest<List<DepositViewModel>>("api/deposit/GetDepositList");
            //var clientDeposits = new List<DepositViewModel>();
            /*foreach (var dep in Deposits)
            {
                if (dep.DepositClients.ContainsKey(clientId))
                {
                    clientDeposits.Add(dep);
                }
            }
            ViewBag.ClientDeposits = clientDeposits;*/
            return View();
        }

        [HttpPost]
        public void CarUpdate(int carId, string name, string discription, DateTime dateOut)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(discription))
            {
                var car = APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}");
                if (car == null)
                {
                    return;
                }
                if (dateOut != DateTime.MinValue)
                {
                    APIInspector.PostRequest("api/car/CreateOrUpdateCar", new CarBindingModel
                    {
                        Id = car.Id,
                        Name = name,
                        Discription = discription,
                        DateIn = car.DateIn,
                        DateOut = dateOut,
                        DefectId = car.DefectId,
                        TechnicalMaintenanceId = car.TechnicalMaintenanceId,
                        InspectorId = Program.Inspector.Id
                    });
                }
                else
                {
                    APIInspector.PostRequest("api/car/CreateOrUpdateCar", new CarBindingModel
                    {
                        Id = car.Id,
                        Name = name,
                        Discription = discription,
                        DateIn = car.DateIn,
                        DateOut= null,
                        DefectId = car.DefectId,
                        TechnicalMaintenanceId = car.TechnicalMaintenanceId,
                        InspectorId = Program.Inspector.Id
                    });
                }
                Response.Redirect("Car");
                return;
            }
            throw new Exception("Введите название авто, описание и дату окончания работ, если она уже известна");
        }

        [HttpGet]
        public void CarDelete(int carId)
        {
            var car = APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}");
            APIInspector.PostRequest("api/car/DeleteCar", car);
            Response.Redirect("Car");
        }

        public IActionResult Defect()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIInspector.GetRequest<List<DefectViewModel>>($"api/inspector/GetInspectorDefectList?inspectorId={Program.Inspector.Id}"));
        }

        [HttpGet]
        public IActionResult DefectCreate()
        {
            ViewBag.Cars = APIInspector.GetRequest<List<CarViewModel>>("api/car/GetCarList");
            return View();
        }

        [HttpPost]
        public void DefectCreate(string name, string discription, List<int> carsId)
        {
            List<CarViewModel> cars = new List<CarViewModel>();
            foreach (var carId in carsId)
            {
                cars.Add(APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}"));
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(discription) && cars != null)
            {
                APIInspector.PostRequest("api/defect/CreateOrUpdateDefect", new DefectBindingModel
                {
                    Name = name,
                    Discription = discription,
                    DefectCars = cars.ToDictionary(x => x.Id, x => x.Name),
                    InspectorId = Program.Inspector.Id
                });
                Response.Redirect("Defect");
                return;
            }
            throw new Exception("Введите наименование, описание неисправности и выберите автомобили");
        }

        [HttpGet]
        public IActionResult DefectUpdate(int defectId)
        {
            ViewBag.Defect = APIInspector.GetRequest<DefectViewModel>($"api/defect/GetDefect?defectId={defectId}");
            ViewBag.Cars = APIInspector.GetRequest<List<CarViewModel>>("api/car/GetCarList");
            return View();
        }

        [HttpPost]
        public void DefectUpdate(int defectId, string name, string discription, List<int> carsId)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(discription) && carsId != null)
            {
                var defect = APIInspector.GetRequest<DefectViewModel>($"api/defect/GetDefect?defectId={defectId}");
                if (defect == null)
                {
                    return;
                }
                List<CarViewModel> cars = new List<CarViewModel>();
                foreach (var carId in carsId)
                {
                    cars.Add(APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}"));
                }
                APIInspector.PostRequest("api/defect/CreateOrUpdateDefect", new DefectBindingModel
                {
                    Id = defect.Id,
                    Name = name,
                    Discription = discription,
                    DefectCars = cars.ToDictionary(x => x.Id, x => x.Name),
                    InspectorId = Program.Inspector.Id
                });
                Response.Redirect("Defect");
                return;
            }
            throw new Exception("Введите наименование, описание неисправности и выберите автомобили");
        }

        [HttpGet]
        public void DefectDelete(int defectId)
        {
            var defect = APIInspector.GetRequest<DefectViewModel>($"api/defect/GetDefect?defectId={defectId}");
            APIInspector.PostRequest("api/defect/DeleteDefect", defect);
            Response.Redirect("Defect");
        }
        /*
        public IActionResult Deposit()
        {
            if (Program.Clerk == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClerk.GetRequest<List<DepositViewModel>>($"api/clerk/GetClerkDepositList?clerkId={Program.Clerk.Id}"));
        }

        [HttpGet]
        public IActionResult DepositCreate()
        {
            return View();
        }

        [HttpPost]
        public void DepositCreate(string depositName, decimal depositInterest)
        {
            if (!string.IsNullOrEmpty(depositName))
            {
                APIClerk.PostRequest("api/deposit/CreateOrUpdateDeposit", new DepositBindingModel
                {
                    DepositName = depositName,
                    DepositInterest = depositInterest,
                    DepositClients = new Dictionary<int, string>(),
                    ClerkId = Program.Clerk.Id
                });
                Response.Redirect("Deposit");
                return;
            }
            throw new Exception("Введите наименование вклада и процентную ставку");
        }

        [HttpGet]
        public IActionResult DepositUpdate(int depositId)
        {
            ViewBag.Deposit = APIClerk.GetRequest<DepositViewModel>($"api/deposit/GetDeposit?depositId={depositId}");
            return View();
        }

        [HttpPost]
        public void DepositUpdate(int depositId, string depositName, decimal depositInterest)
        {
            if (!string.IsNullOrEmpty(depositName) && depositInterest != 0)
            {
                var deposit = APIClerk.GetRequest<DepositViewModel>($"api/deposit/GetDeposit?depositId={depositId}");
                if (deposit == null)
                {
                    return;
                }
                APIClerk.PostRequest("api/deposit/CreateOrUpdateDeposit", new DepositBindingModel
                {
                    Id = deposit.Id,
                    DepositName = depositName,
                    DepositInterest = depositInterest,
                    DepositClients = deposit.DepositClients,
                    ClerkId = Program.Clerk.Id
                });
                Response.Redirect("Deposit");
                return;
            }
            throw new Exception("Введите наименование вклада и процентную ставку");
        }

        [HttpGet]
        public void DepositDelete(int depositId)
        {
            var deposit = APIClerk.GetRequest<DepositViewModel>($"api/deposit/GetDeposit?depositId={depositId}");
            APIClerk.PostRequest("api/deposit/DeleteDeposit", deposit);
            Response.Redirect("Deposit");
        }

        public IActionResult Replenishment()
        {
            if (Program.Clerk == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClerk.GetRequest<List<ReplenishmentViewModel>>($"api/clerk/GetClerkReplenishmentList?clerkId={Program.Clerk.Id}"));
        }

        [HttpGet]
        public IActionResult ReplenishmentCreate()
        {
            ViewBag.Deposits = APIClerk.GetRequest<List<DepositViewModel>>("api/deposit/GetDepositList");
            return View();
        }

        [HttpPost]
        public void ReplenishmentCreate(int replenishmentAmount, int depositId)
        {
            if (replenishmentAmount != 0 && depositId != 0)
            {
                APIClerk.PostRequest("api/replenishment/CreateOrUpdateReplenishment", new ReplenishmentBindingModel
                {
                    Amount = replenishmentAmount,
                    DateReplenishment = DateTime.Now,
                    DepositId = depositId,
                    ClerkId = Program.Clerk.Id
                });
                Response.Redirect("Replenishment");
                return;
            }
            throw new Exception("Введите сумму пополнения и выберите вклад");
        }

        [HttpGet]
        public IActionResult ReplenishmentUpdate(int replenishmentId)
        {
            ViewBag.Deposits = APIClerk.GetRequest<List<DepositViewModel>>("api/deposit/GetDepositList");
            ViewBag.Replenishment = APIClerk.GetRequest<ReplenishmentViewModel>($"api/replenishment/GetReplenishment?replenishmentId={replenishmentId}");
            return View();
        }

        [HttpPost]
        public void ReplenishmentUpdate(int replenishmentId, int replenishmentAmount, int depositId)
        {
            if (replenishmentAmount != 0 && replenishmentAmount != 0 && depositId != 0)
            {
                var replenishment = APIClerk.GetRequest<ReplenishmentViewModel>($"api/replenishment/GetReplenishment?replenishmentId={replenishmentId}");
                if (replenishment == null)
                {
                    return;
                }
                APIClerk.PostRequest("api/replenishment/CreateOrUpdateReplenishment", new ReplenishmentBindingModel
                {
                    Id = replenishment.Id,
                    Amount = replenishmentAmount,
                    DateReplenishment = DateTime.Now,
                    DepositId = depositId,
                    ClerkId = Program.Clerk.Id
                });
                Response.Redirect("Replenishment");
                return;
            }
            throw new Exception("Введите сумму пополнения и выберите вклад");
        }

        [HttpGet]
        public void ReplenishmentDelete(int replenishmentId)
        {
            var replenishment = APIClerk.GetRequest<ReplenishmentViewModel>($"api/replenishment/GetReplenishment?replenishmentId={replenishmentId}");
            APIClerk.PostRequest("api/replenishment/DeleteReplenishment", replenishment);
            Response.Redirect("Replenishment");
        }

        [HttpGet]
        public IActionResult AddDepositClients()
        {
            if (Program.Clerk == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Deposits = APIClerk.GetRequest<List<DepositViewModel>>("api/deposit/GetDepositList");
            ViewBag.Clients = APIClerk.GetRequest<List<ClientViewModel>>($"api/clerk/GetClerkClientList?clerkId={Program.Clerk.Id}");
            return View();
        }

        [HttpPost]
        public void AddDepositClients(int depositId, List<int> clientsId)
        {
            if (depositId != 0 && clientsId != null)
            {
                APIClerk.PostRequest("api/deposit/AddDepositClients", new AddClientsBindingModel
                {
                    DepositId = depositId,
                    ClientsId = clientsId
                });
                Response.Redirect("Deposit");
                return;
            }
            throw new Exception("Выберите вклад и клиентов");
        }
        */
    }
}