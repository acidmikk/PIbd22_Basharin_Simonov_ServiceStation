﻿using ServiceStationContracts.BindingModels;
using ServiceStationContracts.BusinessLogicsContracts;
using ServiceStationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceStationRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarLogic _carLogic;

        public CarController(ICarLogic carLogic)
        {
            _carLogic = carLogic;
        }

        [HttpGet]
        public List<CarViewModel> GetCarList() => _carLogic.Read(null)?.ToList();

        [HttpGet]
        public CarViewModel GetCar(int carId) => _carLogic.Read(new CarBindingModel { Id = carId })?[0];

        [HttpPost]
        public void CreateOrUpdateCar(CarBindingModel model) => _carLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteCar(CarBindingModel model) => _carLogic.Delete(model);
    }
}
