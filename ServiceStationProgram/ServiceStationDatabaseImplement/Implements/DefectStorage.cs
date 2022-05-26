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
    public class DefectStorage : IDefectStorage
    {
        public List<DefectViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();
            return context.Defects
            .Include(rec => rec.Inspector)
            .Include(rec => rec.Repair)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<DefectViewModel> GetFilteredList(DefectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            return context.Defects
            .Include(rec => rec.Repair)
            .Where(rec => (rec.Name.Contains(model.Name)) || (model.InspectorId.HasValue && rec.InspectorId == model.InspectorId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public DefectViewModel GetElement(DefectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();
            var defect = context.Defects
            .Include(rec => rec.Repair)
            .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return defect != null ? CreateModel(defect) : null;
        }
        public void Insert(DefectBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Defect defect = new Defect()
                {
                    Name = model.Name,
                    Discription = model.Discription,
                    InspectorId = (int)model.InspectorId,
                    RepairId = (int)model.RepairId
                };
                context.Defects.Add(defect);
                context.SaveChanges();
                CreateModel(model, defect, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(DefectBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Defects.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(DefectBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            Defect element = context.Defects.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Defects.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Defect CreateModel(DefectBindingModel model, Defect defect, ServiceStationDatabase context)
        {
            defect.Name = model.Name;
            defect.Discription = model.Discription;
            defect.InspectorId = (int)model.InspectorId;
            defect.RepairId = (int)model.RepairId;
            return defect;
        }
        private static DefectViewModel CreateModel(Defect defect)
        {
            return new DefectViewModel
            {
                Id = defect.Id,
                Name = defect.Name,
                Discription = defect.Discription,
                RepairId = defect.RepairId
            };
        }
    }
}
