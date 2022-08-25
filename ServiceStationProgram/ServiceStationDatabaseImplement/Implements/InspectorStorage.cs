using Microsoft.EntityFrameworkCore;
using ServiceStationContracts.BindingModels;
using ServiceStationContracts.StoragesContracts;
using ServiceStationContracts.ViewModels;
using ServiceStationDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationDatabaseImplement.Implements
{
    public class InspectorStorage : IInspectorStorage
    {
        public void Delete(InspectorBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            Inspector element = context.Inspectors.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Inspectors.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Клиент не найден");
            }
        }

        public InspectorViewModel GetElement(InspectorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();

            var inspector = context.Inspectors
            .Include(x => x.Defects)
            .Include(x => x.Cars)
            .Include(x => x.TechnicalMaintenances)
            .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
            return inspector != null ?
            new InspectorViewModel
            {
                Id = inspector.Id,
                InspectorFIO = inspector.InspectorFIO,
                Email = inspector.Email,
                Password = inspector.Password,
            } :
            null;
        }

        public List<InspectorViewModel> GetFilteredList(InspectorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();

            return context.Inspectors
                .Include(x => x.Defects)
                .Include(x => x.Cars)
                .Include(x => x.TechnicalMaintenances)
            .Where(rec => rec.Email == model.Email && rec.Password == model.Password)
            .Select(rec => new InspectorViewModel
            {
                Id = rec.Id,
                InspectorFIO = rec.InspectorFIO,
                Email = rec.Email,
                Password = rec.Password,
            })
            .ToList();
        }

        public List<InspectorViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();

            return context.Inspectors.Select(rec => new InspectorViewModel
            {
                Id = rec.Id,
                InspectorFIO = rec.InspectorFIO,
                Email = rec.Email,
                Password = rec.Password,
            })
            .ToList();
        }

        public void Insert(InspectorBindingModel model)
        {
            using var context = new ServiceStationDatabase();

            context.Inspectors.Add(CreateModel(model, new Inspector()));
            context.SaveChanges();
        }

        public void Update(InspectorBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Inspectors.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Руководитель не найден");
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
        private Inspector CreateModel(InspectorBindingModel model, Inspector inspector)
        {
            inspector.InspectorFIO = model.InspectorFIO;
            inspector.Email = model.Email;
            inspector.Password = model.Password;
            return inspector;
        }
    }
}
