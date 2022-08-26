using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceStationDatabaseImplement.Migrations
{
    public partial class workField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Spares_SparesId",
                table: "Works");

            migrationBuilder.AlterColumn<int>(
                name: "SparesId",
                table: "Works",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Spares_SparesId",
                table: "Works",
                column: "SparesId",
                principalTable: "Spares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Spares_SparesId",
                table: "Works");

            migrationBuilder.AlterColumn<int>(
                name: "SparesId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Spares_SparesId",
                table: "Works",
                column: "SparesId",
                principalTable: "Spares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
