using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceStationDatabaseImplement.Migrations
{
    public partial class defectField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Repairs_RepairId",
                table: "Defects");

            migrationBuilder.AlterColumn<int>(
                name: "RepairId",
                table: "Defects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Repairs_RepairId",
                table: "Defects",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_Repairs_RepairId",
                table: "Defects");

            migrationBuilder.AlterColumn<int>(
                name: "RepairId",
                table: "Defects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_Repairs_RepairId",
                table: "Defects",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
