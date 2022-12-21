using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftLoger.Migrations
{
    public partial class AddedLogType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogType",
                table: "LogModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogType",
                table: "LogModel");
        }
    }
}
