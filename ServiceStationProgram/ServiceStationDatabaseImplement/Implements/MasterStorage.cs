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
    public class MasterStorage
    {
        public void Delete(MasterBindingModel model)
        {
            using var context = new ServiceStationDatabase();
            Master element = context.Masters.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Masters.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Клиент не найден");
            }
        }

        public MasterViewModel GetElement(MasterBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();

            var master = context.Masters
            .Include(x => x.Spares)
            .Include(x => x.Repairs)
            .Include(x => x.Works)
            .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
            return master != null ?
            new MasterViewModel
            {
                Id = master.Id,
                MasterFIO = master.MasterFIO,
                Email = master.Email,
                Password = master.Password,
            } :
            null;
        }

        public List<MasterViewModel> GetFilteredList(MasterBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ServiceStationDatabase();

            return context.Masters
                .Include(x => x.Spares)
                .Include(x => x.Repairs)
                .Include(x => x.Works)
            .Where(rec => rec.Email == model.Email && rec.Password == model.Password)
            .Select(rec => new MasterViewModel
            {
                Id = rec.Id,
                MasterFIO = rec.MasterFIO,
                Email = rec.Email,
                Password = rec.Password,
            })
            .ToList();
        }

        public List<MasterViewModel> GetFullList()
        {
            using var context = new ServiceStationDatabase();

            return context.Masters.Select(rec => new MasterViewModel
            {
                Id = rec.Id,
                MasterFIO = rec.MasterFIO,
                Email = rec.Email,
                Password = rec.Password,
            })
            .ToList();
        }

        public void Insert(MasterBindingModel model)
        {
            using var context = new ServiceStationDatabase();

            context.Masters.Add(CreateModel(model, new Master()));
            context.SaveChanges();
        }

        public void Update(MasterBindingModel model)
        {
            using var context = new ServiceStationDatabase();

            var element = context.Masters.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Руководитель не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        private Master CreateModel(MasterBindingModel model, Master master)
        {
            master.MasterFIO = model.MasterFIO;
            master.Email = model.Email;
            master.Password = model.Password;
            return master;
        }
    }
}
}
