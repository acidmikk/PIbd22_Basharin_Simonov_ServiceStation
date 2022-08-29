﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceStationDatabaseImplement;

#nullable disable

namespace ServiceStationDatabaseImplement.Migrations
{
    [DbContext(typeof(ServiceStationDatabase))]
    partial class ServiceStationDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOut")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DefectId")
                        .HasColumnType("int");

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InspectorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TechnicalMaintenanceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DefectId");

                    b.HasIndex("InspectorId");

                    b.HasIndex("TechnicalMaintenanceId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Defect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InspectorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RepairId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InspectorId");

                    b.HasIndex("RepairId");

                    b.ToTable("Defects");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Inspector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InspectorFIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Inspectors");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Master", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MasterFIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Masters");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MasterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Spares", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DefectId")
                        .HasColumnType("int");

                    b.Property<int>("MasterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RepairId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DefectId");

                    b.HasIndex("MasterId");

                    b.HasIndex("RepairId");

                    b.ToTable("Spares");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.TechnicalMaintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InspectorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InspectorId");

                    b.ToTable("TechnicalMaintenances");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MasterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SparesId")
                        .HasColumnType("int");

                    b.Property<int>("TechnicalMaintenanceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.HasIndex("SparesId");

                    b.HasIndex("TechnicalMaintenanceId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Car", b =>
                {
                    b.HasOne("ServiceStationDatabaseImplement.Models.Defect", "Defect")
                        .WithMany("Cars")
                        .HasForeignKey("DefectId");

                    b.HasOne("ServiceStationDatabaseImplement.Models.Inspector", "Inspector")
                        .WithMany("Cars")
                        .HasForeignKey("InspectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceStationDatabaseImplement.Models.TechnicalMaintenance", "TechnicalMaintenance")
                        .WithMany("Cars")
                        .HasForeignKey("TechnicalMaintenanceId");

                    b.Navigation("Defect");

                    b.Navigation("Inspector");

                    b.Navigation("TechnicalMaintenance");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Defect", b =>
                {
                    b.HasOne("ServiceStationDatabaseImplement.Models.Inspector", "Inspector")
                        .WithMany("Defects")
                        .HasForeignKey("InspectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceStationDatabaseImplement.Models.Repair", "Repair")
                        .WithMany()
                        .HasForeignKey("RepairId");

                    b.Navigation("Inspector");

                    b.Navigation("Repair");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Repair", b =>
                {
                    b.HasOne("ServiceStationDatabaseImplement.Models.Master", "Master")
                        .WithMany("Repairs")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Master");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Spares", b =>
                {
                    b.HasOne("ServiceStationDatabaseImplement.Models.Defect", "Defect")
                        .WithMany("Spares")
                        .HasForeignKey("DefectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceStationDatabaseImplement.Models.Master", "Master")
                        .WithMany("Spares")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceStationDatabaseImplement.Models.Repair", "Repair")
                        .WithMany("Spares")
                        .HasForeignKey("RepairId");

                    b.Navigation("Defect");

                    b.Navigation("Master");

                    b.Navigation("Repair");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.TechnicalMaintenance", b =>
                {
                    b.HasOne("ServiceStationDatabaseImplement.Models.Inspector", "Inspector")
                        .WithMany("TechnicalMaintenances")
                        .HasForeignKey("InspectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inspector");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Work", b =>
                {
                    b.HasOne("ServiceStationDatabaseImplement.Models.Master", "Master")
                        .WithMany("Works")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceStationDatabaseImplement.Models.Spares", "Spares")
                        .WithMany("Works")
                        .HasForeignKey("SparesId");

                    b.HasOne("ServiceStationDatabaseImplement.Models.TechnicalMaintenance", "TechnicalMaintenance")
                        .WithMany()
                        .HasForeignKey("TechnicalMaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Master");

                    b.Navigation("Spares");

                    b.Navigation("TechnicalMaintenance");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Defect", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Spares");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Inspector", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Defects");

                    b.Navigation("TechnicalMaintenances");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Master", b =>
                {
                    b.Navigation("Repairs");

                    b.Navigation("Spares");

                    b.Navigation("Works");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Repair", b =>
                {
                    b.Navigation("Spares");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.Spares", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("ServiceStationDatabaseImplement.Models.TechnicalMaintenance", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
