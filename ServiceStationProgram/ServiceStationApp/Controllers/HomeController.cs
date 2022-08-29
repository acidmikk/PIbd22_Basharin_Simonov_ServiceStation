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
            return View();
        }

        [HttpPost]
        public void CarUpdate(int carId, string name, string discription, DateTime dateOut)
        {
            var car = APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}");
            if (dateOut < car.DateIn)
            {
                throw new Exception("Дата окончания работ должна быть больше, чем дата начала работ");
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(discription))
            {
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
                        DateOut = null,
                        DefectId = car.DefectId,
                        TechnicalMaintenanceId = car.TechnicalMaintenanceId,
                        InspectorId = Program.Inspector.Id
                    });
                }
                Response.Redirect("Car");
                return;
            }
            throw new Exception("Введите название автомобиля, описание");
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
            ViewBag.Cars = APIInspector.GetRequest<List<CarViewModel>>($"api/inspector/GetInspectorCarList?inspectorId={Program.Inspector.Id}");
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
            var cars = APIInspector.GetRequest<List<CarViewModel>>("api/car/GetCarList");
            var defectCars = new List<CarViewModel>();
            foreach (var car in cars)
            {
                if (car.DefectId == defectId)
                {
                    defectCars.Add(car);
                }
            }
            ViewBag.DefectCars = defectCars;
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

        public IActionResult TechnicalMaintenance()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIInspector.GetRequest<List<TechnicalMaintenanceViewModel>>($"api/inspector/GetInspectorTechnicalMaintenanceList?inspectorId={Program.Inspector.Id}"));
        }

        [HttpGet]
        public IActionResult TechnicalMaintenanceCreate()
        {
            ViewBag.Cars = APIInspector.GetRequest<List<CarViewModel>>($"api/inspector/GetInspectorCarList?inspectorId={Program.Inspector.Id}");
            return View();
        }

        [HttpPost]
        public void TechnicalMaintenanceCreate(string name, string discription, List<int> carsId)
        {
            List<CarViewModel> cars = new List<CarViewModel>();
            foreach (var carId in carsId)
            {
                cars.Add(APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}"));
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(discription) && cars != null)
            {
                APIInspector.PostRequest("api/technicalMaintenance/CreateOrUpdateTechnicalMaintenance", new TechnicalMaintenanceBindingModel
                {
                    Name = name,
                    Discription = discription,
                    TechnicalMaintenanceCars = cars.ToDictionary(x => x.Id, x => x.Name),
                    InspectorId = Program.Inspector.Id
                });
                Response.Redirect("TechnicalMaintenance");
                return;
            }
            throw new Exception("Введите наименование, описание TO и выберите автомобили");
        }

        [HttpGet]
        public IActionResult TechnicalMaintenanceUpdate(int technicalMaintenanceId)
        {
            ViewBag.TechnicalMaintenance = APIInspector.GetRequest<TechnicalMaintenanceViewModel>($"api/TechnicalMaintenance/GetTechnicalMaintenance?technicalMaintenanceId={technicalMaintenanceId}");
            ViewBag.Cars = APIInspector.GetRequest<List<CarViewModel>>("api/car/GetCarList");
            var cars = APIInspector.GetRequest<List<CarViewModel>>("api/car/GetCarList");
            var technicalMaintenanceCars = new List<CarViewModel>();
            foreach (var car in cars)
            {
                if (car.TechnicalMaintenanceId == technicalMaintenanceId)
                {
                    technicalMaintenanceCars.Add(car);
                }
            }
            ViewBag.TechnicalMaintenanceCars = technicalMaintenanceCars;
            return View();
        }

        [HttpPost]
        public void TechnicalMaintenanceUpdate(int technicalMaintenanceId, string name, string discription, List<int> carsId)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(discription) && carsId != null)
            {
                var technicalMaintenance = APIInspector.GetRequest<TechnicalMaintenanceViewModel>($"api/TechnicalMaintenance/GetTechnicalMaintenance?technicalMaintenanceId={technicalMaintenanceId}");
                if (technicalMaintenance == null)
                {
                    return;
                }
                List<CarViewModel> cars = new List<CarViewModel>();
                foreach (var carId in carsId)
                {
                    cars.Add(APIInspector.GetRequest<CarViewModel>($"api/car/GetCar?carId={carId}"));
                }
                APIInspector.PostRequest("api/TechnicalMaintenance/CreateOrUpdateTechnicalMaintenance", new TechnicalMaintenanceBindingModel
                {
                    Id = technicalMaintenance.Id,
                    Name = name,
                    Discription = discription,
                    TechnicalMaintenanceCars = cars.ToDictionary(x => x.Id, x => x.Name),
                    InspectorId = Program.Inspector.Id
                });
                Response.Redirect("TechnicalMaintenance");
                return;
            }
            throw new Exception("Введите наименование, описание TO и выберите автомобили");
        }

        [HttpGet]
        public void TechnicalMaintenanceDelete(int technicalMaintenanceId)
        {
            var technicalMaintenance = APIInspector.GetRequest<TechnicalMaintenanceViewModel>($"api/TechnicalMaintenance/GetTechnicalMaintenance?technicalMaintenanceId={technicalMaintenanceId}");
            APIInspector.PostRequest("api/TechnicalMaintenance/DeleteTechnicalMaintenance", technicalMaintenance);
            Response.Redirect("TechnicalMaintenance");
        }

        [HttpGet]
        public IActionResult AddDefectRepair()
        {
            if (Program.Inspector == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Defects = APIInspector.GetRequest<List<DefectViewModel>>($"api/inspector/GetInspectorDefectList?inspectorId={Program.Inspector.Id}");
            ViewBag.Repairs = APIInspector.GetRequest<List<RepairViewModel>>("api/defect/GetRepairList");
            return View();
        }

        [HttpPost]
        public void AddDefectRepair(int defectId, int repairId)
        {
            if (defectId != 0 && repairId != 0)
            {
                APIInspector.PostRequest("api/defect/AddDefectRepair", new AddDefectRepairBindingModel
                {
                    DefectId = defectId,
                    RepairId = repairId
                });
                Response.Redirect("Defect");
                return;
            }
            throw new Exception("Выберите неисправность и ремонт");
        }
    }
}