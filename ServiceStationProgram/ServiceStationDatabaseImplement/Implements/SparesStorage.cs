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
    public class SparesStorage : ISparesStorage
    {
        public List<SparesViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();
            return context.Spares
            .Include(rec => rec.Defect)
            .Include(rec => rec.Repair)
            .Include(rec => rec.Master)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<SparesViewModel> GetFilteredList(SparesBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            return context.Spares
            .Include(rec => rec.Repair)
            .Include(rec => rec.Defect)
            .Where(rec => (rec.Name.Contains(model.Name)) || (model.MasterId.HasValue && rec.MasterId == model.MasterId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public SparesViewModel GetElement(SparesBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            var spares = context.Spares
            .Include(rec => rec.Repair)
            .Include(rec => rec.Defect)
            .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return spares != null ? CreateModel(spares) : null;
        }
        public void Insert(SparesBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Spares spares = new Spares()
                {
                    Name = model.Name,
                    DefectId = (int)model.DefectId,
                    MasterId = (int)model.MasterId,
                    RepairId = (int)model.RepairId
                };
                context.Spares.Add(spares);
                context.SaveChanges();
                CreateModel(model, spares, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(SparesBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Spares.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(SparesBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            Spares element = context.Spares.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Spares.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Spares CreateModel(SparesBindingModel model, Spares spares, ServiceStationDatabase context)
        {
            spares.Name = model.Name;
            spares.MasterId = (int)model.MasterId;
            spares.RepairId = (int)model.RepairId;
            spares.DefectId = (int)model.DefectId;
            return spares;
        }
        private static SparesViewModel CreateModel(Spares spares)
        {
            return new SparesViewModel
            {
                Id = spares.Id,
                Name = spares.Name,
                RepairId = spares.RepairId,
                DefectId = spares.DefectId,
            };
        }
    }
}
