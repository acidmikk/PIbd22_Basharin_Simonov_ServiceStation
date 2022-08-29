﻿using System;
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
    public class TechnicalMaintenanceStorage : ITechnicalMaintenanceStorage
    {
        public List<TechnicalMaintenanceViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();
            return context.TechnicalMaintenances
            .Include(rec => rec.Inspector)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<TechnicalMaintenanceViewModel> GetFilteredList(TechnicalMaintenanceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            return context.TechnicalMaintenances
            .Where(rec => (rec.Name.Contains(model.Name)) || (model.InspectorId.HasValue && rec.InspectorId == model.InspectorId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public TechnicalMaintenanceViewModel GetElement(TechnicalMaintenanceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            var technicalMaintenance = context.TechnicalMaintenances
            .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return technicalMaintenance != null ? CreateModel(technicalMaintenance) : null;
        }
        public void Insert(TechnicalMaintenanceBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                TechnicalMaintenance technicalMaintenance = new TechnicalMaintenance()
                {
                    Name = model.Name,
                    Discription = model.Discription,
                    InspectorId = (int)model.InspectorId
                };
                context.TechnicalMaintenances.Add(technicalMaintenance);
                context.SaveChanges();
                CreateModel(model, technicalMaintenance, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(TechnicalMaintenanceBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.TechnicalMaintenances.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(TechnicalMaintenanceBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            TechnicalMaintenance element = context.TechnicalMaintenances.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                var technicalMaintenanceCars = context.Cars.Where(rec => rec.TechnicalMaintenanceId == model.Id);
                foreach(var car in technicalMaintenanceCars)
                {
                    car.TechnicalMaintenanceId = null;                    
                }
                context.SaveChanges();
                context.TechnicalMaintenances.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static TechnicalMaintenance CreateModel(TechnicalMaintenanceBindingModel model, TechnicalMaintenance technicalMaintenance, ServiceStationDatabase context)
        {
            technicalMaintenance.Name = model.Name;
            technicalMaintenance.Discription = model.Discription;
            technicalMaintenance.InspectorId = (int)model.InspectorId;
            //отвязываем ранее привязанные машины
            var cars = context.Cars.Where(rec => rec.TechnicalMaintenanceId == model.Id).ToList();
            foreach (var car in cars)
            {
                car.TechnicalMaintenanceId = null;
                context.SaveChanges();
            }
            technicalMaintenance.Cars = new List<Car>();
            //берём список добавленных
            foreach (var tmc in model.TechnicalMaintenanceCars)
            {
                //ищем в БД машин
                var technicalMaintenanceCar = context.Cars.FirstOrDefault(c => c.Id == tmc.Key);
                if (technicalMaintenanceCar != null)
                {
                    technicalMaintenance.Cars.Add(technicalMaintenanceCar);
                }
            }
            return technicalMaintenance;
        }
        private static TechnicalMaintenanceViewModel CreateModel(TechnicalMaintenance technicalMaintenance)
        {
            return new TechnicalMaintenanceViewModel
            {
                Id = technicalMaintenance.Id,
                Name = technicalMaintenance.Name,
                Discription = technicalMaintenance.Discription
            };
        }
    }
}
