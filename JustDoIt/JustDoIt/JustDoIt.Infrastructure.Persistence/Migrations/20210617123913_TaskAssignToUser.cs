using Microsoft.EntityFrameworkCore.Migrations;

namespace JustDoIt.Infrastructure.Persistence.Migrations
{
    public partial class TaskAssignToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedToUserId",
                table: "TaskModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedToUserId",
                table: "TaskModels");
        }
    }
}
