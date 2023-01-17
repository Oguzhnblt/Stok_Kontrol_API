using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stok_Kontrol_API.Repositories.Migrations
{
    public partial class initOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_TedarikçiID",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "TedarikçiID",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_TedarikçiID",
                table: "Products",
                column: "TedarikçiID",
                principalTable: "Suppliers",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_TedarikçiID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "TedarikçiID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_TedarikçiID",
                table: "Products",
                column: "TedarikçiID",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
