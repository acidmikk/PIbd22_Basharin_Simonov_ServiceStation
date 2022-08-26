using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceStationContracts.BindingModels;
using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;
using ServiceStationDatabaseImplement.Models;

namespace ServiceStationDatabaseImplement.Implements
{
    public class CarStorage : ICarStorage
    {
        public void Delete(CarBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            Car element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Cars.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            var order = context.Cars
            .Include(rec => rec.Defect)
            .Include(rec => rec.TechnicalMaintenance)
            .Include(rec => rec.Inspector)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            return context.Cars
            .Include(rec => rec.Defect)
            .Include(rec => rec.TechnicalMaintenance)
            .Include(rec => rec.Inspector)
            .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateIn.Date == model.DateIn.Date) ||
            (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateIn.Date >= model.DateFrom.Value.Date && rec.DateIn.Date <= model.DateTo.Value.Date) ||
            (model.InspectorId.HasValue && rec.InspectorId == model.InspectorId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<CarViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();
            return context.Cars
            .Include(rec => rec.Defect)
            .Include(rec => rec.TechnicalMaintenance)
            .Include(rec => rec.Inspector)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(CarBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Cars.Add(CreateModel(model, new Car()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(CarBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Cars.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private static Car CreateModel(CarBindingModel model, Car car)
        {
            car.DefectId = model.DefectId;
            car.TechnicalMaintenanceId = model.TechnicalMaintenanceId;
            car.InspectorId = (int)model.InspectorId;
            car.DateIn = model.DateIn;
            car.DateOut = model.DateOut;
            car.Name = model.Name;
            car.Discription = model.Discription;
            return car;
        }
        private static CarViewModel CreateModel(Car car)
        {
            return new CarViewModel
            {
                Id = car.Id,
                DefectId = car.DefectId,
                TechnicalMaintenanceId = car.TechnicalMaintenanceId,
                Name = car.Name,
                DateIn = car.DateIn,
                DateOut = car.DateOut,
                DefectName = car.Defect?.Name,
                TechnicalMaintenanceName = car.TechnicalMaintenance?.Name,
                Discription = car.Discription
            };
        }
    }
}
