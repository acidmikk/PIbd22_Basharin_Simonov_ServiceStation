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
    public class WorkStorage : IWorkStorage
    {
        public List<WorkViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();
            return context.Works
            .Include(rec => rec.Master)
            .Include(rec => rec.Spares)
            .Include(rec => rec.TechnicalMaintenance)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<WorkViewModel> GetFilteredList(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            return context.Works
            .Include(rec => rec.Spares)
            .Include(rec => rec.TechnicalMaintenance)
            .Include(rec => rec.Master)
            .Where(rec => (rec.Name.Contains(model.Name)) || (model.MasterId.HasValue && rec.MasterId == model.MasterId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public WorkViewModel GetElement(WorkBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            var work = context.Works
            .Include(rec => rec.Spares)
            .Include(rec => rec.TechnicalMaintenance)
            .Include(rec => rec.Master)
            .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return work != null ? CreateModel(work) : null;
        }
        public void Insert(WorkBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Work work = new Work()
                {
                    Name = model.Name,
                    MasterId = (int)model.MasterId,
                    SparesId = (int)model.SparesId,
                    TechnicalMaintenanceId = (int)model.TechnicalMaintenanceId
                };
                context.Works.Add(work);
                context.SaveChanges();
                CreateModel(model, work, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(WorkBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Works.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(WorkBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            Work element = context.Works.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Works.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Work CreateModel(WorkBindingModel model, Work work, ServiceStationDatabase context)
        {
            work.Name = model.Name;
            work.MasterId = (int)model.MasterId;
            work.TechnicalMaintenanceId = (int)model.TechnicalMaintenanceId;
            work.SparesId = (int)model.SparesId;
            return work;
        }
        private static WorkViewModel CreateModel(Work work)
        {
            return new WorkViewModel
            {
                Id = work.Id,
                Name = work.Name,
                TechnicalMaintenanceId = work.TechnicalMaintenanceId,
                SparesId = work.SparesId
            };
        }
    }
}
