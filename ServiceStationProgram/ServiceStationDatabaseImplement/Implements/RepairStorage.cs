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
    public class RepairStorage : IRepairStorage
    {
        public List<RepairViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();
            return context.Repairs
            .Include(rec => rec.Master)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<RepairViewModel> GetFilteredList(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            return context.Repairs
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public RepairViewModel GetElement(RepairBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            var repair = context.Repairs
            .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return repair != null ? CreateModel(repair) : null;
        }
        public void Insert(RepairBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Repair repair = new Repair()
                {
                    Name = model.Name,
                    MasterId = (int)model.MasterId
                };
                context.Repairs.Add(repair);
                context.SaveChanges();
                CreateModel(model, repair, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(RepairBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(RepairBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            Repair element = context.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Repairs.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Repair CreateModel(RepairBindingModel model, Repair repair, ServiceStationDatabase context)
        {
            repair.Name = model.Name;
            repair.MasterId = (int)model.MasterId;
            return repair;
        }
        private static RepairViewModel CreateModel(Repair repair)
        {
            return new RepairViewModel
            {
                Id = repair.Id,
                Name = repair.Name,
            };
        }
    }
}
