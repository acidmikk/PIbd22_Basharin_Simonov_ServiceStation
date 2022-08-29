using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceStationDatabaseImplement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inspectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectorFIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterFIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalMaintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalMaintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalMaintenances_Inspectors_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepairId = table.Column<int>(type: "int", nullable: true),
                    InspectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Defects_Inspectors_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Defects_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefectId = table.Column<int>(type: "int", nullable: true),
                    TechnicalMaintenanceId = table.Column<int>(type: "int", nullable: true),
                    InspectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Defects_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Inspectors_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_TechnicalMaintenances_TechnicalMaintenanceId",
                        column: x => x.TechnicalMaintenanceId,
                        principalTable: "TechnicalMaintenances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Spares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefectId = table.Column<int>(type: "int", nullable: false),
                    RepairId = table.Column<int>(type: "int", nullable: true),
                    MasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spares_Defects_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spares_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spares_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SparesId = table.Column<int>(type: "int", nullable: true),
                    TechnicalMaintenanceId = table.Column<int>(type: "int", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Works_Spares_SparesId",
                        column: x => x.SparesId,
                        principalTable: "Spares",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_TechnicalMaintenances_TechnicalMaintenanceId",
                        column: x => x.TechnicalMaintenanceId,
                        principalTable: "TechnicalMaintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DefectId",
                table: "Cars",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InspectorId",
                table: "Cars",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TechnicalMaintenanceId",
                table: "Cars",
                column: "TechnicalMaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_InspectorId",
                table: "Defects",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_RepairId",
                table: "Defects",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_MasterId",
                table: "Repairs",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Spares_DefectId",
                table: "Spares",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Spares_MasterId",
                table: "Spares",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Spares_RepairId",
                table: "Spares",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalMaintenances_InspectorId",
                table: "TechnicalMaintenances",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_MasterId",
                table: "Works",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_SparesId",
                table: "Works",
                column: "SparesId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_TechnicalMaintenanceId",
                table: "Works",
                column: "TechnicalMaintenanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Spares");

            migrationBuilder.DropTable(
                name: "TechnicalMaintenances");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "Inspectors");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Masters");
        }
    }
}
