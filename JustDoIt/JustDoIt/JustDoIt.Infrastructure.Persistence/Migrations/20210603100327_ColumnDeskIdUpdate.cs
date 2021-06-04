using Microsoft.EntityFrameworkCore.Migrations;

namespace JustDoIt.Infrastructure.Persistence.Migrations
{
    public partial class ColumnDeskIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Desks_DeskId",
                table: "Columns");

            migrationBuilder.AlterColumn<int>(
                name: "DeskId",
                table: "Columns",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Desks_DeskId",
                table: "Columns",
                column: "DeskId",
                principalTable: "Desks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Columns_Desks_DeskId",
                table: "Columns");

            migrationBuilder.AlterColumn<int>(
                name: "DeskId",
                table: "Columns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Columns_Desks_DeskId",
                table: "Columns",
                column: "DeskId",
                principalTable: "Desks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
