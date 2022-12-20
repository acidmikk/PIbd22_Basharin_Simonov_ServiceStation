using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceStationDatabaseImplement.Models;

namespace ServiceStationDatabaseImplement
{
    public class ServiceStationDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=ServiceStationDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().Property(m => m.DefectId).IsRequired(false);
            modelBuilder.Entity<Car>().Property(m => m.TechnicalMaintenanceId).IsRequired(false);
            modelBuilder.Entity<Defect>().Property(m => m.RepairId).IsRequired(false);
            modelBuilder.Entity<Work>().Property(m => m.SparesId).IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Car> Cars { set; get; }
        public virtual DbSet<Defect> Defects { set; get; }
        public virtual DbSet<Inspector> Inspectors { set; get; }
        public virtual DbSet<Master> Masters { set; get; }
        public virtual DbSet<Repair> Repairs { set; get; }
        public virtual DbSet<Spares> Spares { set; get; }
        public virtual DbSet<TechnicalMaintenance> TechnicalMaintenances { set; get; }
        public virtual DbSet<Work> Works { set; get; }
    }
}
