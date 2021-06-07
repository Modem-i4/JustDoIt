using Microsoft.EntityFrameworkCore.Migrations;

namespace JustDoIt.Infrastructure.Persistence.Migrations
{
    public partial class TaskUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentTaskId",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_ParentTaskId",
                table: "Tasks",
                column: "ParentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModels_TaskModels_ParentTaskId",
                table: "Tasks",
                column: "ParentTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModels_TaskModels_ParentTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_TaskModels_ParentTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ParentTaskId",
                table: "Tasks");
        }
    }
}
